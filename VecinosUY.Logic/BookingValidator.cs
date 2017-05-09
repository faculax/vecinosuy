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
using VecinosUY.Logger;

namespace VecinosUY.Logic
{
    public class BookingValidator : IBookingValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public BookingValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Booking> GetBookings()
        {
            return unitOfWork.BookingRepository.Get();
        }

        public Booking GetBooking(string id)
        {
            Booking booking = null;
            booking = unitOfWork.BookingRepository.GetByID(id);
            if (booking == null)
            {
                throw new NotExistException("La reserva especificada no existe");
            }
            return booking;
        }

        public Booking PutBooking(string bookingId, Booking booking)
        {
            booking.BookingId = bookingId;
            Booking oldBooking = GetBooking(bookingId);
            if (oldBooking != null)
            {
                oldBooking.User = booking.User;
                oldBooking.Service = booking.Service;
                oldBooking.BookedFrom = booking.BookedFrom;
                oldBooking.BookedTo = booking.BookedTo;
                unitOfWork.BookingRepository.Update(oldBooking);
                unitOfWork.Save();
            }
            else
            {
                throw new NotExistException("La reserva especificada no existe");
            }
            return booking;
        }

        public void PostBooking(Booking booking)
        {
            unitOfWork.BookingRepository.Insert(booking);
            unitOfWork.Save();
        }


        public void DeleteBooking(string bookingId)
        {
            Booking booking = GetBooking(bookingId);
            if (booking != null)
            {
                unitOfWork.BookingRepository.Update(booking);
                unitOfWork.Save();
                //this.PutBooking(bookingId, booking);
            }
            else
            {
                throw new NotExistException("La reserva especificada no existe");
            }

        }

        private bool BookingExists(int id)
        {
            return unitOfWork.BookingRepository.GetByID(id) != null; ;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void secure(HttpRequestMessage request)
        {
            Security.Security.secure(request);
        }

        public void AtmSecure(HttpRequestMessage request)
        {
            Security.Security.AtmSecure(request);
        }
    }
}
