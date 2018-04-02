using System;
using System.Collections.Generic;
using System.Text;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation error exception thrown when there is an error when executing validation through a validation attribute.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class ValidationErrorException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="validationAttributeType">Type of the validation attribute.</param>
        /// <param name="validationSourceType">Type of the validation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidationErrorException(string message, Type validationAttributeType, Type validationSourceType, string propertyName, Exception innerException)
            : base(message, innerException)
        {
            message.CannotBeNullOrEmpty();
            innerException.CannotBeNull();
            validationAttributeType.CannotBeNull();
            validationSourceType.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            this.ValidationAttributeType = validationAttributeType;
            this.ValidationSourceType = validationSourceType;
            this.PropertyName = propertyName;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidationErrorException"/> class from being created.
        /// </summary>
        private ValidationErrorException()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName
        {
            get;
        }

        /// <summary>
        /// Gets the type of the validation attribute.
        /// </summary>
        /// <value>
        /// The type of the validation attribute.
        /// </value>
        public Type ValidationAttributeType
        {
            get;
        }

        /// <summary>
        /// Gets the type of the validation source.
        /// </summary>
        /// <value>
        /// The type of the validation source.
        /// </value>
        public Type ValidationSourceType
        {
            get;
        }

        #endregion Public Properties
    }
}
