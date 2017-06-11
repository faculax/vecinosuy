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

        
       
    }
}
