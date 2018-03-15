using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid date string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidDateAttribute : ValidationAttribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public string DateFormat
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <returns>
        /// The default message.
        /// </returns>
        public override string GetDefaultMessage()
        {
            return "Value must be a valid date.";
        }

        /// <summary>
        /// Gets the default message key used for localizing the message when using resource files.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeValidDate";
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

                if (this.DateFormat != null)
                {
                    if (DateTime.TryParseExact(value as string, this.DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateValue))
                    {
                        return true;
                    }
                }
                else
                {
                    if (DateTime.TryParse(value as string, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateValue))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #endregion Public Methods
    }
}
