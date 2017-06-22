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
