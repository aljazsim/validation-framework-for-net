using System;
using System.Collections.Generic;
using System.Threading;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is equal to the specified value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeEqualToAttribute : ValidationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeEqualToAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public MustBeEqualToAttribute(object value)
            : this()
        {
            this.ComparedValue = value;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MustBeEqualToAttribute"/> class from being created.
        /// </summary>
        private MustBeEqualToAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the compared value.
        /// </summary>
        /// <value>
        /// The compared value.
        /// </value>
        public object ComparedValue
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
            return "Value must be equal to {0}.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustBeEqualTo";
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
                return this.ComparedValue == null;
            }
            else if (value == DBNull.Value)
            {
                return this.ComparedValue == DBNull.Value;
            }
            else
            {
                value.MustBeSubTypeOf(typeof(IComparable));

                if (this.ComparedValue == null)
                {
                    return value == null;
                }
                else if (this.ComparedValue == DBNull.Value)
                {
                    return value == DBNull.Value;
                }
                else
                {
                    value.MustBeTypeOf(this.ComparedValue.GetType());

                    if (value is sbyte)
                    {
                        return ((sbyte)value).CompareTo(Convert.ToSByte((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is byte)
                    {
                        return ((byte)value).CompareTo(Convert.ToByte((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is short)
                    {
                        return ((short)value).CompareTo(Convert.ToInt16((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is ushort)
                    {
                        return ((ushort)value).CompareTo(Convert.ToUInt16((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is int)
                    {
                        return ((int)value).CompareTo(Convert.ToInt32((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is uint)
                    {
                        return ((uint)value).CompareTo(Convert.ToUInt32((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is long)
                    {
                        return ((long)value).CompareTo(Convert.ToInt64((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is ulong)
                    {
                        return ((ulong)value).CompareTo(Convert.ToUInt64((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is float)
                    {
                        return ((float)value).CompareTo(Convert.ToSingle((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is double)
                    {
                        return ((double)value).CompareTo(Convert.ToDouble((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else if (value is decimal)
                    {
                        return ((decimal)value).CompareTo(Convert.ToDecimal((object)this.ComparedValue, Thread.CurrentThread.CurrentCulture)) == 0;
                    }
                    else
                    {
                        value.MustBeTypeOf(this.ComparedValue.GetType());

                        return ((IComparable)value).CompareTo(this.ComparedValue) == 0;
                    }
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
            return new object[] { this.ComparedValue };
        }

        #endregion Protected Methods
    }
}
