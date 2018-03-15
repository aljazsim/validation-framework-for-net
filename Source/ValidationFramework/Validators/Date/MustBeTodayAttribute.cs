using System;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is today's date.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeTodayAttribute : ValidationAttribute
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
            return "Value must be today.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeToday";
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
                value.MustBeTypeOf(typeof(DateTime));

                var dateValue = (DateTime)value;

                return dateValue.Date == DateTime.Today.Date;
            }
        }

        #endregion Public Methods
    }
}
