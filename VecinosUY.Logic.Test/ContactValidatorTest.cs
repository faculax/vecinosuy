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
    public class ContactValidatorTest
    {
        [TestMethod]
        public void GetAllContactFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.ContactRepository.Get(null, null, ""));

            ContactValidator contactValidator = new ContactValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Contact> returnedContact = contactValidator.GetContacts();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetContactById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del ContactRepository con un int
            mockUnitOfWork.Setup(un => un.ContactRepository.GetByID(It.IsAny<int>()));

            ContactValidator contactValidator = new ContactValidator(mockUnitOfWork.Object);
            //Act

            Contact returnedContact = contactValidator.GetContact("5");

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateContactTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Booking y luego al Save();
            mockUnitOfWork.Setup(un => un.ContactRepository.Insert(It.IsAny<Contact>()));
            mockUnitOfWork.Setup(un => un.Save());

            ContactValidator contactValidator = new ContactValidator(mockUnitOfWork.Object);

            //Act
            contactValidator.PostContact(new Contact());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingContact()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.ContactRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.ContactRepository.Update(It.IsAny<Contact>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IContactValidator contactValidator = new ContactValidator(mockUnitOfWork.Object);

            //act
            contactValidator.PutContact("0", new Contact() { });

            //Assert
            //Retorna exception
        }

      
    }
}
