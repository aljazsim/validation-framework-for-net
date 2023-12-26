using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidationFramework.Examples.Ef
{
    public abstract class Entity : Validatable
    {
        private static int _newIdSequence = 0;

        #region Public Constructors

        public Entity()
        {
            this.Id = --_newIdSequence;
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
