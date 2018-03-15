using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value contains duplicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustContainDuplicatesAttribute : ValidationAttribute
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
            return "Value must contain duplicates.";
        }

        /// <summary>
        /// Gets the default message key.
        /// </summary>
        /// <returns>
        /// The default message key.
        /// </returns>
        public override string GetDefaultMessageKey()
        {
            return "MustContainDuplicates";
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
                value.MustBeSubTypeOf(typeof(IEnumerable));

                var iterator = (value as IEnumerable).GetEnumerator();
                var duplicates = new HashSet<object>();

                while (iterator.MoveNext())
                {
                    if (duplicates.Contains(iterator.Current))
                    {
                        return true;
                    }
                    else
                    {
                        duplicates.Add(iterator.Current);
                    }
                }

                return false;
            }
        }

        #endregion Public Methods
    }
}
