using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using VecinosUY.Data.DataAccess;
using VecinosUY.Data.Entities;
using VecinosUY.Exceptions;
using VecinosUY.Logger;
using VecinosUY.PlainTextLogger;

namespace VecinosUY.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private VecinosUYContext context;
        private GenericRepository<User> userRepository;
        private GenericRepository<Property> propertieRepository;
        private GenericRepository<Building> buildingRepository;
        private GenericRepository<Service> serviceRepository;
        private GenericRepository<Booking> bookingRepository;
        private GenericRepository<FavoriteAdds> favoriteAddsRepository;
        private ILogger logger;

        public UnitOfWork(VecinosUYContext VecinosUYContext)
        {
            context = VecinosUYContext;
        }

        public IRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public IRepository<Property> PropertyRepository
        {
            get
            {

                if (this.propertieRepository == null)
                {
                    this.propertieRepository = new GenericRepository<Property>(context);
                }
                return propertieRepository;
            }
        }


        public ILogger Logger
        {
            get
            {
                if (this.logger == null) {
                    this.logger = new PlainTextLog();
                }
                return this.logger;
            }
        }


        public void Save()
        {
            try
            { 
                context.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
               
            }
}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
