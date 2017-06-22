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
    public class UserValidatorTest
    {
        

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetUserByIdReturnsUserWithId()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del userRepository con un int
            mockUnitOfWork.Setup(un => un.UserRepository.GetByID(It.IsAny<int>()));

            UserValidator userValidator = new UserValidator(mockUnitOfWork.Object);
            //Act
            
            User returnedUser = userValidator.GetUser("5");

            //Assert
            //mockUnitOfWork.VerifyAll();
        }

       
        
        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingUser()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>())).Returns(() => null);
            
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IUserValidator userValidator = new UserValidator(mockUnitOfWork.Object);

            //act
            userValidator.PutUser("0",new User() { });

            //Assert
            //Retorna exception
        }

       
    }
}
