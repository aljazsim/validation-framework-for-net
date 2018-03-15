using System;
using System.Collections.Generic;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not subtype of specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeSubTypeOfAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeSubTypeOfAttribute" /> class.
        /// </summary>
        /// <param name="type">The invalid type.</param>
        public CannotBeSubTypeOfAttribute(Type type)
                    : this()
        {
            this.Type = type;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="CannotBeSubTypeOfAttribute"/> class from being created.
        /// </summary>
        private CannotBeSubTypeOfAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the invalid type.
        /// </summary>
        /// <value>
        /// The invalid type.
        /// </value>
        public Type Type
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
            return "Value cannot be sub-type of {0}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "CannotBeSubTypeOf";
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
            this.Type.CannotBeNull();

            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                return !value.IsSubTypeOf(this.Type);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Gets the message parameters. This method should be overriden in a subclass
        /// if additional message parameters are to be presented in a message (e. g.
        /// uper limit of a string length).
        /// </summary>
        /// <returns>Array of parameters.</returns>
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Type.Name };
        }

        #endregion Protected Methods
    }
}
