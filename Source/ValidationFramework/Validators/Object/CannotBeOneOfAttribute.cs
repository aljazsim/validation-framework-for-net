using System;
using System.Collections.Generic;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value does not belong to the specified set of values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeOneOfAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeOneOfAttribute" /> class.
        /// </summary>
        /// <param name="set">The set of invalid values.</param>
        public CannotBeOneOfAttribute(params object[] set)
            : this()
        {
            set.CannotBeNullOrEmpty();

            this.Set = set;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="CannotBeOneOfAttribute"/> class from being created.
        /// </summary>
        private CannotBeOneOfAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the set of invalid values.
        /// </summary>
        /// <value>
        /// The set of invalid values.
        /// </value>
        public IEnumerable<object> Set
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
            return "Value cannot be one of: {0}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "CannotBeOneOf";
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
            if (value == null)
            {
                return !this.Set.Any(x => x == null);
            }
            else if (value == DBNull.Value)
            {
                return !this.Set.Any(x => x == DBNull.Value);
            }
            else
            {
                return !this.Set.Any(x => ((IComparable)value).CompareTo(x) == 0);
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
            return new object[] { string.Join(", ", this.Set.Select(x => x.ToString())) };
        }

        #endregion Protected Methods
    }
}
