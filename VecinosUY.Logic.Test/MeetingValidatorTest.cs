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
    public class MeetingValidatorTest
    {
        [TestMethod]
        public void GetAllMeetingsFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.MeetingRepository.Get(null, null, ""));

            MeetingValidator meetingValidator = new MeetingValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Meeting> returnedMeeting = meetingValidator.GetMeetings();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetMeetingById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del MeetingRepository con un int
            mockUnitOfWork.Setup(un => un.MeetingRepository.GetByID(It.IsAny<int>()));

            MeetingValidator meetingValidator = new MeetingValidator(mockUnitOfWork.Object);
            //Act

            Meeting returnedMeeting = meetingValidator.GetMeetingsById(5);

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateMeetingTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Meeting y luego al Save();
            mockUnitOfWork.Setup(un => un.MeetingRepository.Insert(It.IsAny<Meeting>()));
            mockUnitOfWork.Setup(un => un.Save());

            MeetingValidator meetingValidator = new MeetingValidator(mockUnitOfWork.Object);

            //Act
            meetingValidator.PostMeeting(new Meeting());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingMeeting()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.MeetingRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.MeetingRepository.Update(It.IsAny<Meeting>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IMeetingValidator meetingValidator = new MeetingValidator(mockUnitOfWork.Object);

            //act
            meetingValidator.PutMeeting(0, new Meeting() { });

            //Assert
            //Retorna exception
        }

        [TestMethod]
        public void UpdatesExistingBooking()
        {
            Meeting meeting = new Meeting
            {
                MeetingId = 1,
                Date = new DateTime(2017, 10, 01),
                Subject = "Subject",
                Deleted = false
            };
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
    .Setup(un => un.MeetingRepository.GetByID(It.IsAny<int>()))
    .Returns(meeting);

            //Además, seteamos las expectativas para los métodos que deben llamarse luego
            mockUnitOfWork.Setup(un => un.MeetingRepository.Update(It.IsAny<Meeting>()));
            mockUnitOfWork.Setup(un => un.Save());

            MeetingValidator meetingValidator = new MeetingValidator(mockUnitOfWork.Object);

            //act
            meetingValidator.PutMeeting(1, meeting);

            //Assert
            mockUnitOfWork.Verify(un => un.MeetingRepository.Update(It.IsAny<Meeting>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
        }
    }
}
