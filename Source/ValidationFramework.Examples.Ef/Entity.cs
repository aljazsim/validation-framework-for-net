using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidationFramework.Examples.Ef
{
    public abstract class Entity : Validatable
    {
        #region Public Constructors

        public Entity()
        {
            this.Id = -1;
        }

        #endregion Public Constructors

        #region Public Properties

        [Column("id")]
        [Key]
        public int Id
        {
            get;
            set;
        }

        #endregion Public Properties
    }
}
