using System.Collections.Generic;
using System.Reflection;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// Property data.
    /// </summary>
    public class PropertyData
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyData"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="validationAttributes">The validation attributes.</param>
        public PropertyData(PropertyInfo propertyInfo, IEnumerable<ValidationAttribute> validationAttributes)
            : this()
        {
            propertyInfo.CannotBeNull();
            validationAttributes.CannotBeNull();

            this.PropertyInfo = propertyInfo;
            this.ValidationAttributes = validationAttributes;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="PropertyData"/> class from being created.
        /// </summary>
        private PropertyData()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <value>
        /// The property information.
        /// </value>
        public PropertyInfo PropertyInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IEnumerable<ValidationAttribute> ValidationAttributes
        {
            get;
            private set;
        }

        #endregion Public Properties
    }
}
