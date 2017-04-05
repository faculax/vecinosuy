using VecinosUY.Data.Entities;
using System.Data.Entity;

namespace VecinosUY.Data.DataAccess
{
    public class VecinosUYContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public VecinosUYContext() : base("VecinosUYContext")
        {
            Database.SetInitializer<VecinosUYContext>(new DropCreateDatabaseIfModelChanges<VecinosUYContext>());
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
    

        }
    }
}
