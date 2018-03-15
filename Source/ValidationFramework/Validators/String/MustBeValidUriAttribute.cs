using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid URI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidUriAttribute : ValidationAttribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the value can contain host only (no paths after domain).
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value can contain host only; otherwise, <c>false</c>.
        /// </value>
        public bool HostOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the valid schemas.
        /// </summary>
        /// <value>
        /// The valid schemas.
        /// </value>
        public IEnumerable<string> ValidSchemas
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
            return "Value must be a valid URI.";
        }

        /// <summary>
        /// Gets the default message key used for localizing the message when using resource files.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeValidUri";
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

                string stringValue = (string)value;

                if (Uri.TryCreate(value as string, UriKind.Absolute, out Uri uri))
                {
                    if (this.ValidSchemas != null &&
                        this.ValidSchemas.Count() > 0 &&
                        !this.ValidSchemas.Contains(uri.Scheme))
                    {
                        return false;
                    }

                    if (this.HostOnly)
                    {
                        if (uri.AbsolutePath != "/")
                        {
                            return false;
                        }
                    }

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
