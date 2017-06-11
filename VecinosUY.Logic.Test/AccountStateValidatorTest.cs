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
    public class AccountStateValidatorTest
    {
        [TestMethod]
        public void GetAllAccountStatesFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.AccountStateRepository.Get(null, null, ""));

            AccountStateValidator accountStateValidator = new AccountStateValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<AccountState> returnedAccountStates = accountStateValidator.GetAccountStates();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetAccountStateByUserId()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del AccountStateRepository con un int
            mockUnitOfWork.Setup(un => un.AccountStateRepository.GetByID(It.IsAny<int>()));

            AccountStateValidator accountStateValidator = new AccountStateValidator(mockUnitOfWork.Object);
            //Act

            IEnumerable<AccountState> returnedAccountState = accountStateValidator.GetAccountStatesById("5");

            //Assert
            //mockUnitOfWork.VerifyAll();
        }

        //public void GetAccountState()
        //{
        //    //Arrange
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    //Esperamos que se llame al metodo Get del AccountStateRepository con un int
        //    mockUnitOfWork.Setup(un => un.AccountStateRepository.GetByID(It.IsAny<int>()));

        //    AccountStateValidator accountStateValidator = new AccountStateValidator(mockUnitOfWork.Object);
        //    //Act

        //    AccountState returnedAccountState = accountStateValidator.GetAccountState("5", 10, 2016);

        //    //Assert
        //    //mockUnitOfWork.VerifyAll();
        //}


        [TestMethod]
        public void CreateAccountStateTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un AccountState y luego al Save();
            mockUnitOfWork.Setup(un => un.AccountStateRepository.Insert(It.IsAny<AccountState>()));
            mockUnitOfWork.Setup(un => un.Save());

            AccountStateValidator accountStateValidator = new AccountStateValidator(mockUnitOfWork.Object);

            //Act
            accountStateValidator.PostAccountState(new AccountState());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

       
    }
}
