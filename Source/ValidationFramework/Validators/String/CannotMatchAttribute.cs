using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value does not match the specified regular expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotMatchAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotMatchAttribute"/> class.
        /// </summary>
        /// <param name="regex">The regregular expression.</param>
        /// <param name="regexOptions">The rgular expression options.</param>
        public CannotMatchAttribute(string regex, RegexOptions regexOptions = RegexOptions.None)
            : this()
        {
            regex.CannotBeNullOrEmpty();

            this.Regex = new Regex(regex, regexOptions);
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="CannotMatchAttribute"/> class from being created.
        /// </summary>
        private CannotMatchAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the regular expression.
        /// </summary>
        /// <value>
        /// The regular expression.
        /// </value>
        public Regex Regex
        {
            get;
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
            return "Value cannot match {0}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "CannotMatch";
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

                return !this.Regex.IsMatch(value as string);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Gets the message parameters. This method should be overriden in a subclass
        /// if additional message parameters are to be presented in a message (e. g.
        /// uper limit of a string length).
        /// </summary>
        /// <returns>
        /// Array of parameters.
        /// </returns>
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Regex.ToString() };
        }

        #endregion Protected Methods
    }
}
