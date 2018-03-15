using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The extension methods for performing property validaiton.
    /// </summary>
    public static class ValidatableExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks if the specified property of the specified validation source is valid.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <c>true</c> if the specified property name is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this IValidatable validationSource, string propertyName)
        {
            propertyName.CannotBeNull();
            validationSource.CannotBeNull();

            return !validationSource.Validate(propertyName).Any(x => x.ValidationLevel == ValidationLevel.Error);
        }

        /// <summary>
        /// Checks if the specified specified validation source is valid (has no invalid properties).
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <returns>
        ///   <c>true</c> if the specified validation source is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this IValidatable validationSource)
        {
            validationSource.CannotBeNull();

            return !validationSource.Validate().Any(x => x.ValidationLevel == ValidationLevel.Error);
        }

        /// <summary>
        /// Validates the specified property of the specified validation source.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The collection of validation mesasges.</returns>
        public static ValidationMessageCollection Validate(this IValidatable validationSource, string propertyName)
        {
            validationSource.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            ValidationMessageCollection messages = new ValidationMessageCollection();
            List<string> validationContexts;
            object propertyValue;

            // get property value
            var properties = ReflectionExtensions.GetProperties(validationSource.GetType());

            if (properties.TryGetValue(propertyName, out PropertyData propertyData))
            {
                if (propertyData.PropertyInfo.CanRead &&
                    propertyData.PropertyInfo.CanWrite)
                {
                    propertyValue = propertyData.PropertyInfo.GetValue(validationSource);

                    // get validation context
                    validationContexts = new List<string>();
                    validationContexts.Add(ValidationContext.Default); // always add the default validation context
                    validationContexts.AddRange(validationSource.GetActiveValidationContexts() ?? Array.Empty<string>()); // add currently active validation context

                    foreach (var validationContext in validationContexts.Distinct())
                    {
                        messages.AddRange(validationSource.Validate(propertyName, propertyValue, validationContext));
                    }
                }
            }

            return messages;
        }

        /// <summary>
        /// Validates the the specified validation source.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <returns>
        /// The collection of validation mesasges.
        /// </returns>
        public static ValidationMessageCollection Validate(this IValidatable validationSource)
        {
            validationSource.CannotBeNull();

            ValidationMessageCollection messages = new ValidationMessageCollection();
            var propertyNames = ReflectionExtensions.GetProperties(validationSource.GetType()).Keys;

            foreach (var propertyName in propertyNames)
            {
                messages.AddRange(validationSource.Validate(propertyName));
            }

            return messages;
        }

        /// <summary>
        /// Validates the specified property of the specified validation source for the specified property value in specified validation context by using validation attributes.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The collection of validation mesasges.
        /// </returns>
        public static ValidationMessageCollection ValidateAttributes(this IValidatable validationSource, string propertyName, object propertyValue, string validationContext)
        {
            validationSource.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            ValidationMessageCollection messages = new ValidationMessageCollection();
            var validationAttributes = ReflectionExtensions.GetProperties(validationSource.GetType())[propertyName].ValidationAttributes;
            bool isValid;

            // perform attribute based validation
            foreach (var validationAttribute in validationAttributes)
            {
                // only matching validation context gets validated
                if (validationAttribute.ValidationContext == validationContext)
                {
                    // custom validators might cause exceptions that are hard to find
                    try
                    {
                        isValid = validationAttribute.IsValid(propertyValue);
                    }
                    catch (Exception ex)
                    {
                        throw new ValidationErrorException("Unhandeled validation exception occured.", validationAttribute.GetType(), validationSource.GetType(), propertyName, ex);
                    }

                    if (!isValid)
                    {
                        var messageKey = validationAttribute.MessageKey ?? validationAttribute.GetDefaultMessageKey() ?? "UndefinedMessageKey";
                        var message = validationAttribute.Message ?? validationAttribute.GetDefaultMessage() ?? "Undefined message.";
                        var messageParameters = validationAttribute.MessageParameters;
                        var validationLevel = validationAttribute.ValidationLevel;
                        var validationPriority = validationAttribute.ValidationPriority;

                        // value is invalid -> add it to the list
                        messages.Add(new ValidationMessage(messageKey, message, messageParameters, validationSource, propertyName, validationLevel, validationContext, validationPriority));
                    }
                }
            }

            return messages;
        }

        #endregion Public Methods
    }
}
