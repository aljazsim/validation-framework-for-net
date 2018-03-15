using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ValidationFramework
{
    /// <summary>
    /// The base class for deriving validation attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidationAttribute : Attribute
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        protected ValidationAttribute()
        {
            // set the defaults
            this.ValidationLevel = ValidationLevel.Error;
            this.ValidationContext = ValidationFramework.ValidationContext.Default;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message key used for localizing the message when using resource files.
        /// </summary>
        /// <value>The message key.</value>
        public string MessageKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the message parameters used when formatting the message.
        /// </summary>
        /// <value>The message parameters.</value>
        public IEnumerable<object> MessageParameters
        {
            get
            {
                return this.GetParameters();
            }
        }

        /// <summary>
        /// Gets or sets the name of the validation context.
        /// </summary>
        /// <value>The name of the validation context.</value>
        public string ValidationContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the level of validation.
        /// </summary>
        /// <value>The level of validation.</value>
        public ValidationLevel ValidationLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the validation priority.
        /// </summary>
        /// <value>The validation priority.</value>
        public int ValidationPriority
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <returns>The default message.</returns>
        public abstract string GetDefaultMessage();

        /// <summary>
        /// Gets the default message key used for localizing the message when using resource files.
        /// </summary>
        /// <returns>The default message key.</returns>
        public abstract string GetDefaultMessageKey();

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsValid(object value);

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Gets the message parameters. This method should be overriden in a subclass
        /// if additional message parameters are to be presented in a message (e. g.
        /// uper limit of a string length).
        /// </summary>
        /// <returns>Array of parameters.</returns>
        protected virtual IEnumerable<object> GetParameters()
        {
            return new List<object>();
        }

        #endregion Protected Methods
    }
}
