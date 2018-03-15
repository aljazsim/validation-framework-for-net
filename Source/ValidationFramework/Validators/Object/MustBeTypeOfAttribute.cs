using System;
using System.Collections.Generic;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is of specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeTypeOfAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeTypeOfAttribute" /> class.
        /// </summary>
        /// <param name="type">The valid type.</param>
        public MustBeTypeOfAttribute(Type type)
                    : this()
        {
            this.Type = type;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MustBeTypeOfAttribute"/> class from being created.
        /// </summary>
        private MustBeTypeOfAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the valid type.
        /// </summary>
        /// <value>
        /// The valid type.
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
            return "Value must be type of {0}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeTypeOf";
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
                return value.GetType() == this.Type;
            }
        }

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

        #endregion Public Methods
    }
}
