using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VecinosUY.Data.Entities;
using VecinosUY.Data.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VecinosUY.Exceptions;
using VecinosUY.Data.Repository;
using System.Net.Http;

namespace VecinosUY.Logic
{
    public interface IBookingValidator : IDisposable
    {
        IEnumerable<Booking> GetBookings();
        Booking GetBooking(string id);
        Booking PutBooking(string bookingId, Booking booking);
        void PostBooking(Booking booking);
        void DeleteBooking(string BookingId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);
    }
}
