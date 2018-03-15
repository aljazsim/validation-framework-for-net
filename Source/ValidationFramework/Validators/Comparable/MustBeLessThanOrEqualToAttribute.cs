using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is less than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeLessThanOrEqualToAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(int maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(ushort maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(byte maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(ulong maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(short maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(sbyte maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(long maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(uint maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(decimal maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(float maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(double maxValue)
            : this()
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MustBeLessThanOrEqualToAttribute(IComparable maxValue)
            : this()
        {
            maxValue.CannotBeNull();

            this.MaxValue = maxValue;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MustBeLessThanOrEqualToAttribute"/> class from being created.
        /// </summary>
        private MustBeLessThanOrEqualToAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public IComparable MaxValue
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the default validation message, that must be implemented in a subclass.
        /// </summary>
        /// <returns>
        /// The default message.
        /// </returns>
        public override string GetDefaultMessage()
        {
            return "Value must be less than or equal to {0}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeLessThanOrEqualTo";
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
                this.MaxValue.CannotBeNull();

                if (value is sbyte)
                {
                    return ((sbyte)value).CompareTo(Convert.ToSByte((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is byte)
                {
                    return ((byte)value).CompareTo(Convert.ToByte((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is short)
                {
                    return ((short)value).CompareTo(Convert.ToInt16((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is ushort)
                {
                    return ((ushort)value).CompareTo(Convert.ToUInt16((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is int)
                {
                    return ((int)value).CompareTo(Convert.ToInt32((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is uint)
                {
                    return ((uint)value).CompareTo(Convert.ToUInt32((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is long)
                {
                    return ((long)value).CompareTo(Convert.ToInt64((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is ulong)
                {
                    return ((ulong)value).CompareTo(Convert.ToUInt64((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is float)
                {
                    return ((float)value).CompareTo(Convert.ToSingle((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is double)
                {
                    return ((double)value).CompareTo(Convert.ToDouble((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else if (value is decimal)
                {
                    return ((decimal)value).CompareTo(Convert.ToDecimal((object)this.MaxValue, Thread.CurrentThread.CurrentCulture)) <= 0;
                }
                else
                {
                    value.MustBeTypeOf(this.MaxValue.GetType());

                    return ((IComparable)value).CompareTo(this.MaxValue) <= 0;
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
        /// <returns>Array of parameters.</returns>
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MaxValue };
        }

        #endregion Protected Methods
    }
}
