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
    public class ServiceValidatorTest
    {
        [TestMethod]
        public void GetAllServicesFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.ServiceRepository.Get(null, null, ""));

            ServiceValidator ServiceValidator = new ServiceValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Service> returnedService = ServiceValidator.GetServices();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetServiceById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del ServiceRepository con un int
            mockUnitOfWork.Setup(un => un.ServiceRepository.GetByID(It.IsAny<int>()));

            ServiceValidator ServiceValidator = new ServiceValidator(mockUnitOfWork.Object);
            //Act

            Service returnedService = ServiceValidator.GetService("5");

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateServiceTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Service y luego al Save();
            mockUnitOfWork.Setup(un => un.ServiceRepository.Insert(It.IsAny<Service>()));
            mockUnitOfWork.Setup(un => un.Save());

            ServiceValidator ServiceValidator = new ServiceValidator(mockUnitOfWork.Object);

            //Act
            ServiceValidator.PostService(new Service());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingService()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.ServiceRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.ServiceRepository.Update(It.IsAny<Service>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IServiceValidator ServiceValidator = new ServiceValidator(mockUnitOfWork.Object);

            //act
            ServiceValidator.PutService("0", new Service() { });

            //Assert
            //Retorna exception
        }

        [TestMethod]
        public void UpdatesExistingBooking()
        {
            Service Service = new Service
            {
                ServiceId = "1",
                Name = "Lavadero",
                Building = "1"
            };
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
    .Setup(un => un.ServiceRepository.GetByID(It.IsAny<int>()))
    .Returns(Service);

            //Además, seteamos las expectativas para los métodos que deben llamarse luego
            mockUnitOfWork.Setup(un => un.ServiceRepository.Update(It.IsAny<Service>()));
            mockUnitOfWork.Setup(un => un.Save());

            ServiceValidator ServiceValidator = new ServiceValidator(mockUnitOfWork.Object);

            //act
            ServiceValidator.PutService("1", Service);

            //Assert
            mockUnitOfWork.Verify(un => un.ServiceRepository.Update(It.IsAny<Service>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
        }
    }
}
