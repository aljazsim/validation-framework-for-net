using System;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// Validation exception is thrown when an operation is performed on an object that is not valid.
    /// </summary>
    public sealed class ValidationException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException" /> class.
        /// </summary>
        /// <param name="validationMessages">The validation messages.</param>
        public ValidationException(ValidationMessageCollection validationMessages)
            : this()
        {
            validationMessages.CannotBeNull();

            this.ValidaitonMessages = validationMessages;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidationException"/> class from being created.
        /// </summary>
        private ValidationException()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>The validation messages.</value>
        public ValidationMessageCollection ValidaitonMessages
        {
            get;
            private set;
        }

        #endregion Public Properties
    }
}
