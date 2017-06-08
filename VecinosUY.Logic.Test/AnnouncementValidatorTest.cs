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
    public class AnnouncementValidatorTest
    {
        [TestMethod]
        public void GetAllAnnouncementFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.AccountStateRepository.Get(null, null, ""));

            AnnouncementValidator announcementValidator = new AnnouncementValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Announcement> returnedAnnouncement = announcementValidator.GetAnnouncements();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetAnnouncementById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del AnnouncementRepository con un int
            mockUnitOfWork.Setup(un => un.AnnouncementRepository.GetByID(It.IsAny<int>()));

            AnnouncementValidator announcementValidator = new AnnouncementValidator(mockUnitOfWork.Object);
            //Act

            Announcement returnedAnnouncement = announcementValidator.GetAnnouncement(5);

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateAnnouncementTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Announcement y luego al Save();
            mockUnitOfWork.Setup(un => un.AnnouncementRepository.Insert(It.IsAny<Announcement>()));
            mockUnitOfWork.Setup(un => un.Save());

            AnnouncementValidator announcementValidator = new AnnouncementValidator(mockUnitOfWork.Object);

            //Act
            announcementValidator.PostAnnouncement(new Announcement());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingAnnouncement()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.AnnouncementRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.AnnouncementRepository.Update(It.IsAny<Announcement>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IAnnouncementValidator announcementValidator = new AnnouncementValidator(mockUnitOfWork.Object);

            //act
            announcementValidator.PutAnnouncement(0, new Announcement() { });

            //Assert
            //Retorna exception
        }

        [TestMethod]
        public void UpdatesExistingAnnouncement()
        {
            Announcement announcement = new Announcement
            {
                AnnouncementId = 1,
                Title = "Title",
                Body = "Body",
                Deleted = false,
                Image = null
            };
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
    .Setup(un => un.AnnouncementRepository.GetByID(It.IsAny<int>()))
    .Returns(announcement);

            //Además, seteamos las expectativas para los métodos que deben llamarse luego
            mockUnitOfWork.Setup(un => un.AnnouncementRepository.Update(It.IsAny<Announcement>()));
            mockUnitOfWork.Setup(un => un.Save());

            AnnouncementValidator announcementValidator = new AnnouncementValidator(mockUnitOfWork.Object);

            //act
            announcementValidator.PutAnnouncement(1, announcement);

            //Assert
            mockUnitOfWork.Verify(un => un.AnnouncementRepository.Update(It.IsAny<Announcement>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
        }
    }
}
