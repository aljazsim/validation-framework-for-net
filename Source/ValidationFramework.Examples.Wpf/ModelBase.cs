using System;
using System.Collections.Generic;
using System.ComponentModel;
using ReactiveUI;

namespace ValidationFramework.Examples.Wpf
{
    /// <summary>
    /// The model.
    /// </summary>
    /// <seealso cref="ReactiveUI.ReactiveObject" />
    /// <seealso cref="ValidationFramework.IValidatable" />
    public abstract class ModelBase : ReactiveObject, IValidatable, IDataErrorInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error
        {
            get
            {
                if (this.SupressValidation)
                {
                    return string.Empty;
                }
                else
                {
                    return this.Validate().Merge(this).Message ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to supress validation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supress validation; otherwise, <c>false</c>.
        /// </value>
        public bool SupressValidation
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Indexers

        /// <summary>
        /// Gets the <see cref="string"/> with the specified property name.
        /// </summary>
        /// <value>
        /// The <see cref="string"/>.
        /// </value>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The validation error.</returns>
        public string this[string propertyName]
        {
            get
            {
                if (this.SupressValidation)
                {
                    return string.Empty;
                }
                else
                {
                    return this.Validate(propertyName).Merge(this, propertyName).Message ?? string.Empty;
                }
            }
        }

        #endregion Public Indexers

        #region Public Methods

        /// <summary>
        /// Gets the list of currently active validation contexts.
        /// </summary>
        /// <returns>
        /// The list of currently active validation contexts.
        /// </returns>
        public virtual IEnumerable<string> GetActiveValidationContexts()
        {
            return Array.Empty<string>();
        }

        /// <summary>
        /// Validates the specified property name for the specified validation context.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The list of validation messages.
        /// </returns>
        public virtual ValidationMessageCollection Validate(string propertyName, object propertyValue, string validationContext)
        {
            return ValidatableExtensions.ValidateAttributes(this, propertyName, propertyValue, validationContext);
        }

        #endregion Public Methods
    }
}
