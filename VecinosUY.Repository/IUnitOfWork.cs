using System;
using VecinosUY.Data.Entities;
using VecinosUY.Logger;

namespace VecinosUY.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Building> BuildingRepository { get; }
        IRepository<Service> ServiceRepository { get; }
        IRepository<Booking> BookingRepository { get; }
        IRepository<FavoriteAdds> FavoriteAddsRepository { get; }

        IRepository<Announcement> AnnouncementRepository { get; }

        IRepository<Property> PropertyRepository { get; }

        ILogger Logger { get; }
        void Save();

    }
}
