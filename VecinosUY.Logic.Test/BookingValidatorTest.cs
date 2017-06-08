using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VecinosUY.Data.Repository;
using VecinosUY.Logic;
using VecinosUY.Data.Entities;
using System.Collections.Generic;
using VecinosUY.Exceptions;

namespace VecinosUY.Logic.Test
{
    [TestClass]
    public class BookingValidatorTest
    {
        [TestMethod]
        public void GetAllBookingsFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.BookingRepository.Get(null, null, ""));

            BookingValidator bookingValidator = new BookingValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Booking> returnedBooking = bookingValidator.GetBookings();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetBookingById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del AnnouncementRepository con un int
            mockUnitOfWork.Setup(un => un.BookingRepository.GetByID(It.IsAny<int>()));

            BookingValidator bookingValidator = new BookingValidator(mockUnitOfWork.Object);
            //Act

            Booking returnedBooking = bookingValidator.GetBooking("5");

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateBookingTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Booking y luego al Save();
            mockUnitOfWork.Setup(un => un.BookingRepository.Insert(It.IsAny<Booking>()));
            mockUnitOfWork.Setup(un => un.Save());

            BookingValidator bookingValidator = new BookingValidator(mockUnitOfWork.Object);

            //Act
            bookingValidator.PostBooking(new Booking());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingBooking()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.BookingRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.BookingRepository.Update(It.IsAny<Booking>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IBookingValidator bookingValidator = new BookingValidator(mockUnitOfWork.Object);

            //act
            bookingValidator.PutBooking("0", new Booking() { });

            //Assert
            //Retorna exception
        }

        [TestMethod]
        public void UpdatesExistingBooking()
        {
            Booking booking = new Booking
            {
                BookingId = 1,
                User = "1",
                Service = "1",
                BookedFrom = new DateTime(2017, 10, 01),
                BookedTo = new DateTime(2017, 10, 02)
            };
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
    .Setup(un => un.BookingRepository.GetByID(It.IsAny<int>()))
    .Returns(booking);

            //Además, seteamos las expectativas para los métodos que deben llamarse luego
            mockUnitOfWork.Setup(un => un.BookingRepository.Update(It.IsAny<Booking>()));
            mockUnitOfWork.Setup(un => un.Save());

            BookingValidator bookingValidator = new BookingValidator(mockUnitOfWork.Object);

            //act
            bookingValidator.PutBooking("1", booking);

            //Assert
            mockUnitOfWork.Verify(un => un.BookingRepository.Update(It.IsAny<Booking>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
        }
    }
}
