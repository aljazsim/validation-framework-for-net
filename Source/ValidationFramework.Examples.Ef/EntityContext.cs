using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ValidationFramework.Examples.Ef
{
    internal class EntityContext : DbContext
    {

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

        public override int SaveChanges()
        {
            ValidationMessageCollection validationMessages;

            validationMessages = new ValidationMessageCollection();

            foreach (EntityEntry entry in this.ChangeTracker.Entries().Where(x => x.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.Entity is Entity)
                {
                    validationMessages.AddRange((entry.Entity as Entity)!.Validate());
                }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDb");
        }
    }
}
