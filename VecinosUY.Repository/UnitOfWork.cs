﻿using System;
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
        private GenericRepository<Announcement> announcementRepository;
        private GenericRepository<AccountState> accountStateRepository;
        private GenericRepository<Property> propertieRepository;
        private GenericRepository<Building> buildingRepository;
        private GenericRepository<Service> serviceRepository;
        private GenericRepository<Booking> bookingRepository;
        private GenericRepository<FavoriteAdds> favoriteAddsRepository;
        private GenericRepository<Meeting> meetingRepository;
        private GenericRepository<Vote> voteRepository;
        private GenericRepository<Contact> contactRepository;
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

        public IRepository<Building> BuildingRepository
        {
            get
            {

                if (this.buildingRepository == null)
                {
                    this.buildingRepository = new GenericRepository<Building>(context);
                }
                return buildingRepository;
            }
        }

        public IRepository<Service> ServiceRepository
        {
            get
            {

                if (this.serviceRepository == null)
                {
                    this.serviceRepository = new GenericRepository<Service>(context);
                }
                return serviceRepository;
            }
        }

        public IRepository<Booking> BookingRepository
        {
            get
            {

                if (this.bookingRepository == null)
                {
                    this.bookingRepository = new GenericRepository<Booking>(context);
                }
                return bookingRepository;
            }
        }

        public IRepository<FavoriteAdds> FavoriteAddsRepository
        {
            get
            {

                if (this.favoriteAddsRepository == null)
                {
                    this.favoriteAddsRepository = new GenericRepository<FavoriteAdds>(context);
                }
                return favoriteAddsRepository;
            }
        }

        public IRepository<Meeting> MeetingRepository
        {
            get
            {

                if (this.meetingRepository == null)
                {
                    this.meetingRepository = new GenericRepository<Meeting>(context);
                }
                return meetingRepository;
            }
        }
        public IRepository<Announcement> AnnouncementRepository
        {
            get
            {

                if (this.announcementRepository == null)
                {
                    this.announcementRepository = new GenericRepository<Announcement>(context);
                }
                return announcementRepository;
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

        public IRepository<AccountState> AccountStateRepository
        {
            get
            {

                if (this.accountStateRepository == null)
                {
                    this.accountStateRepository = new GenericRepository<AccountState>(context);
                }
                return accountStateRepository;
            }
        }
        public IRepository<Vote> VoteRepository
        {
            get
            {

                if (this.voteRepository == null)
                {
                    this.voteRepository = new GenericRepository<Vote>(context);
                }
                return voteRepository;
            }
        }

        public IRepository<Contact> ContactRepository
        {
            get
            {

                if (this.contactRepository == null)
                {
                    this.contactRepository = new GenericRepository<Contact>(context);
                }
                return contactRepository;
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
            catch (Exception e) {
                context.AccountStates.Local.Clear();
                context.Users.Local.Clear();
                context.Services.Local.Clear();
                throw e;
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
