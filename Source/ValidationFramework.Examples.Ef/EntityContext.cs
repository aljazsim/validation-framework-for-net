using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ValidationFramework.Examples.Ef
{
    internal class EntityContext : DbContext
    {
        #region Public Constructors

        public EntityContext()
            : base("sqlite")
        {
            Database.SetInitializer<EntityContext>(new CreateDatabaseIfNotExists<EntityContext>());
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<Item> Items
        {
            get;
            set;
        }

        public DbSet<Order> Orders
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public override int SaveChanges()
        {
            ValidationMessageCollection validationMessages;

            validationMessages = new ValidationMessageCollection();

            foreach (DbEntityEntry entry in this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added ||
                                                                                    x.State == EntityState.Modified))
            {
                validationMessages.AddRange((entry.Entity as Entity).Validate());
            }

            if (validationMessages.Any(x => x.ValidationLevel == ValidationLevel.Error))
            {
                // we found invalid objects that were being saved => prevent saving them to the database
                throw new ValidationException(validationMessages);
            }
            else
            {
                return base.SaveChanges();
            }
        }

        #endregion Public Methods
    }
}
