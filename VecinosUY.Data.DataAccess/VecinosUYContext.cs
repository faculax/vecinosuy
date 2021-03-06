﻿using VecinosUY.Data.Entities;
using System.Data.Entity;

namespace VecinosUY.Data.DataAccess
{
    public class VecinosUYContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings  { get; set; }
        public DbSet<FavoriteAdds> FavoriteAdds { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<AccountState> AccountStates { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Contact> Contacts { get; set; }

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
