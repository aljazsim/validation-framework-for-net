using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid integer number string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidIntegerAttribute : ValidationAttribute
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
            return "Value must be a valid integer.";
        }

        /// <summary>
        /// Gets the default message key used for localizing the message when using resource files.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeValidInteger";
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
                return true;
            }
            else
            {
                value.MustBeTypeOf(typeof(string));

                if (int.TryParse(value as string, NumberStyles.Integer, CultureInfo.CurrentCulture, out int integerValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion Public Methods
    }
}
