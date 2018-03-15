using System;
using System.Collections;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not null or whitespace.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeNullOrWhitespaceAttribute : ValidationAttribute
    {
        #region Public Methods

        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <returns>
        /// The default message.
        /// </returns>
        public override string GetDefaultMessage()
        {
            return "Value cannot be null or white space.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "CannotBeNullOrWhitespace";
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return false;
            }
            else
            {
                value.MustBeTypeOf(typeof(string));

                return !string.IsNullOrWhiteSpace(value as string);
            }
        }

        #endregion Public Methods
    }
}
