using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid time span string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidTimeSpanAttribute : ValidationAttribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the time span format.
        /// </summary>
        /// <value>
        /// The time span format.
        /// </value>
        public string TimeSpanFormat
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
            return "Value must be a valid time span.";
        }

        /// <summary>
        /// Gets the default message key used for localizing the message when using resource files.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeValidTimeSpan";
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

                if (this.TimeSpanFormat != null)
                {
                    if (TimeSpan.TryParseExact(value as string, this.TimeSpanFormat, CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan timeSpan))
                    {
                        return true;
                    }
                }
                else
                {
                    if (TimeSpan.TryParse(value as string, CultureInfo.CurrentCulture, out TimeSpan timeSpan))
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
