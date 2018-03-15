using System.ComponentModel.DataAnnotations.Schema;

namespace ValidationFramework.Examples.Ef
{
    [Table("item")]
    public class Item : Entity
    {
        #region Public Properties

        [CannotBeLongerThan(10)]
        [CannotBeNullOrWhitespace]
        [Column("name")]
        public string Name
        {
            get;
            set;
        }

        [CannotBeNull]
        public Order Order
        {
            get;
            set;
        }

        [Column("order_id")]
        [ForeignKey(nameof(Order))]
        public int OrderId
        {
            get; set;
        }

        [MustBePreciseToDecimalPlaces(2)]
        [MustBeGreaterThanOrEqualTo(0)]
        [Column("price")]
        public decimal Price
        {
            get;
            set;
        }

        #endregion Public Properties
    }
}
