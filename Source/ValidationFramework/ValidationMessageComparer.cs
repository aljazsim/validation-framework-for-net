using System;
using System.Collections.Generic;
using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation message comparer.
    /// </summary>
    public class ValidationMessageComparer : IComparer<ValidationMessage>, IEqualityComparer<ValidationMessage>
    {
        #region Private Fields

        /// <summary>
        /// The equal result.
        /// </summary>
        private const int Equal = 0;

        /// <summary>
        /// The greater result.
        /// </summary>
        private const int Greater = 1;

        /// <summary>
        /// The less result.
        /// </summary>
        private const int Less = -1;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value
        /// Condition
        /// Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// Zero
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// Greater than zero
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        public int Compare(ValidationMessage x, ValidationMessage y)
        {
            if (x == null &&
                y == null)
            {
                return Equal;
            }
            else if (x == null &&
                     y != null)
            {
                return Less;
            }
            else if (x != null &&
                     y == null)
            {
                return Greater;
            }
            else
            {
                if (Compare(x.ValidationSource.GetType().ToString(), y.ValidationSource.GetType().ToString()) == Equal)
                {
                    if (Compare(x.PropertyName, y.PropertyName) == Equal)
                    {
                        if (x.ValidationLevel.CompareTo(y.ValidationLevel) == Equal)
                        {
                            if (x.ValidationPriority.CompareTo(y.ValidationPriority) == Equal)
                            {
                                if (Compare(x.ValidationContext, y.ValidationContext) == Equal)
                                {
                                    return Compare(x.Message, y.Message);
                                }
                                else
                                {
                                    return Compare(x.ValidationContext, y.ValidationContext);
                                }
                            }
                            else
                            {
                                return x.ValidationPriority.CompareTo(y.ValidationPriority);
                            }
                        }
                        else
                        {
                            return x.ValidationLevel.CompareTo(y.ValidationLevel);
                        }
                    }
                    else
                    {
                        return Compare(x.PropertyName, y.PropertyName);
                    }
                }
                else
                {
                    return Compare(x.ValidationSource.GetType().ToString(), y.ValidationSource.GetType().ToString());
                }
            }
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref type="T"/> to compare.</param>
        /// <param name="y">The second object of type <paramref type="T"/> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(ValidationMessage x, ValidationMessage y)
        {
            return x == y;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(ValidationMessage obj)
        {
            return obj.GetHashCode();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Compares x to y.
        /// </summary>
        /// <param name="x">The x string.</param>
        /// <param name="y">The y string.</param>
        /// <returns>The comparisson of x and y strings.</returns>
        private static int Compare(string x, string y)
        {
            if (x == null &&
                y == null)
            {
                return Equal;
            }
            else if (x == null &&
                     y != null)
            {
                return Less;
            }
            else if (x != null &&
                     y == null)
            {
                return Greater;
            }
            else
            {
                return string.Compare(x, y, StringComparison.InvariantCulture);
            }
        }

        #endregion Private Methods
    }
}
