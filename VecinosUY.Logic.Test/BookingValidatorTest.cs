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

        
    }
}
