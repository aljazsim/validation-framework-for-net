using System;
using System.Collections.Generic;
using System.Threading;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding value is not between specified limits.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CannotBeBetweenAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(byte minValue, byte maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(char minValue, char maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(double minValue, double maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(float minValue, float maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(long minValue, long maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(int minValue, int maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(IComparable minValue, IComparable maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(sbyte minValue, sbyte maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(short minValue, short maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(uint minValue, uint maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(ulong minValue, ulong maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(ushort minValue, ushort maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotBeBetweenAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="inclusive">If set to <c>true</c> include limits in the range.</param>
        public CannotBeBetweenAttribute(decimal minValue, decimal maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Inclusive = inclusive;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="CannotBeBetweenAttribute" /> class from being created.
        /// </summary>
        private CannotBeBetweenAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether minimum and maximum values are included in the range.
        /// </summary>
        /// <value>
        ///   <c>true</c> if inclusive; otherwise, <c>false</c>.
        /// </value>
        public bool Inclusive
        {
            get;
        }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public IComparable MaxValue
        {
            get;
        }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public IComparable MinValue
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
            return "Value cannot be between {0} and {1}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "CannotBeBetween";
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
                return true;
            }
            else
            {
                this.MinValue.CannotBeNull();
                this.MaxValue.CannotBeNull();
                this.MinValue.MustBeTypeOf(this.MaxValue.GetType());
                this.MinValue.MustBeLessThanOrEqualTo(this.MaxValue);

                if (value is sbyte)
                {
                    return !((sbyte)value).IsBetween(Convert.ToSByte((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToSByte((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is byte)
                {
                    return !((byte)value).IsBetween(Convert.ToByte((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToByte((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is short)
                {
                    return !((short)value).IsBetween(Convert.ToInt16((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToInt16((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is ushort)
                {
                    return !((ushort)value).IsBetween(Convert.ToUInt16((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToUInt16((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is int)
                {
                    return !((int)value).IsBetween(Convert.ToInt32((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToInt32((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is uint)
                {
                    return !((uint)value).IsBetween(Convert.ToUInt32((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToUInt32((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is long)
                {
                    return !((long)value).IsBetween(Convert.ToInt64((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToInt64((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is ulong)
                {
                    return !((ulong)value).IsBetween(Convert.ToUInt64((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToUInt64((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is float)
                {
                    return !((float)value).IsBetween(Convert.ToSingle((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToSingle((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is double)
                {
                    return !((double)value).IsBetween(Convert.ToDouble((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToDouble((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else if (value is decimal)
                {
                    return !((decimal)value).IsBetween(Convert.ToDecimal((object)this.MinValue, Thread.CurrentThread.CurrentCulture), Convert.ToDecimal((object)this.MaxValue, Thread.CurrentThread.CurrentCulture), this.Inclusive);
                }
                else
                {
                    value.MustBeTypeOf(this.MinValue.GetType());
                    value.MustBeTypeOf(this.MaxValue.GetType());

                    return !(value as IComparable).IsBetween(this.MinValue, this.MaxValue, this.Inclusive);
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
            return new object[] { this.MinValue, this.MaxValue };
        }

        #endregion Protected Methods
    }
}
