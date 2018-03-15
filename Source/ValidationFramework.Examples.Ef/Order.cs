using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ValidationFramework.Examples.Ef
{
    /// <summary>
    /// The order.
    /// </summary>
    /// <seealso cref="ValidationFramework.Examples.Ef.Entity" />
    [Table("order")]
    public class Order : Entity
    {
        #region Public Constructors

        public Order()
        {
            this.Items = new List<Item>();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [CannotBeNullOrEmpty]
        public ICollection<Item> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Column("price")]
        public decimal Price
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The list of validation messages.
        /// </returns>
        public override ValidationMessageCollection Validate(string propertyName, object value, string validationContext)
        {
            ValidationMessageCollection messages = base.Validate(propertyName, value, validationContext);

            if (propertyName == nameof(this.Price) &&
                value is decimal)
            {
                if ((decimal)value != this.Items.Sum(x => x.Price))
                {
                    messages.Add(new ValidationMessage("MustEqualSum", "The order price must match the sum of the item prices.", Array.Empty<object>(), this, propertyName));
                }
            }

            return messages;
        }

        #endregion Public Methods
    }
}
