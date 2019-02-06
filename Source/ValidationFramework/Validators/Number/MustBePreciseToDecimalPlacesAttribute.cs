using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is precise to the specified number of decimal places.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBePreciseToDecimalPlacesAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBePreciseToDecimalPlacesAttribute"/> class.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        public MustBePreciseToDecimalPlacesAttribute(int decimalPlaces)
            : this()
        {
            decimalPlaces.MustBeGreaterThanOrEqualTo(0);

            this.DecimalPlaces = decimalPlaces;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MustBePreciseToDecimalPlacesAttribute"/> class from being created.
        /// </summary>
        private MustBePreciseToDecimalPlacesAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the number of decimal places.
        /// </summary>
        /// <value>
        /// The number of decimal places.
        /// </value>
        public int DecimalPlaces
        {
            get;
            private set;
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
            return "Value must be precise to {0} decimal places.";
        }

        /// <summary>
        /// Gets the default message key used for localizing the message when using resource files.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBePreciseToDecimalPlaces";
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
            Type valueType;

            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                this.DecimalPlaces.MustBeGreaterThanOrEqualTo(0);

                valueType = value.GetType();

                if (valueType == typeof(decimal))
                {
                    decimal coefficient = (decimal)Math.Pow(10, this.DecimalPlaces);

                    return (decimal)value == Math.Round((decimal)value * coefficient) / coefficient;
                }
                else if (valueType == typeof(double))
                {
                    double coefficient = Math.Pow(10, this.DecimalPlaces);

                    return (double)value == Math.Round((double)value * coefficient) / coefficient;
                }
                else if (valueType == typeof(float))
                {
                    float coefficient = (float)Math.Pow(10, this.DecimalPlaces);

                    return (float)value == (float)(Math.Round((float)value * coefficient) / coefficient);
                }
                else if (valueType == typeof(byte) ||
                         valueType == typeof(short) ||
                         valueType == typeof(int) ||
                         valueType == typeof(long) ||
                         valueType == typeof(ushort) ||
                         valueType == typeof(uint) ||
                         valueType == typeof(ulong))
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException("Invalid value type.");
                }
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
            return new object[] { this.DecimalPlaces };
        }

        #endregion Protected Methods
    }
}
