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
        public void GetAllUsersFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.UserRepository.Get(null, null, ""));

            UserValidator userValidator = new UserValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<User> returnedUsers = userValidator.GetUsers();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

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
        public void CreateUserTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del userRepository con un Usuario y luego al Save();
            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());

            UserValidator userValidator = new UserValidator(mockUnitOfWork.Object);

            //Act
            userValidator.PostUser(new User());

            //Assert
            mockUnitOfWork.VerifyAll();

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

        [TestMethod]
        public void UpdatesExistingUser()
        {
            User user = new User
            {
                Name = "Luis",
                Admin = true,
                Deleted = false,
                Password = "luis",
                UserId = "1"
            };
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
                        mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(user);

            //Además, seteamos las expectativas para los métodos que deben llamarse luego
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());

            UserValidator userValidator = new UserValidator(mockUnitOfWork.Object);

            //act
            userValidator.PutUser("1",user);

            //Assert
            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));            
        }
    }
}
