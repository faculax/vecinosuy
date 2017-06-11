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
    public class BuildingValidatorTest
    {
        [TestMethod]
        public void GetAllBuildingsFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.BuildingRepository.Get(null, null, ""));

            BuildingValidator buildingValidator = new BuildingValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Building> returnedBuilding = buildingValidator.GetBuildings();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetBuildingById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del BuildingRepository con un int
            mockUnitOfWork.Setup(un => un.BuildingRepository.GetByID(It.IsAny<int>()));

            BuildingValidator buildingValidator = new BuildingValidator(mockUnitOfWork.Object);
            //Act

            Building returnedBuilding = buildingValidator.GetBuilding("5");

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateBuildingTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Building y luego al Save();
            mockUnitOfWork.Setup(un => un.BuildingRepository.Insert(It.IsAny<Building>()));
            mockUnitOfWork.Setup(un => un.Save());

            BuildingValidator buildingValidator = new BuildingValidator(mockUnitOfWork.Object);

            //Act
            buildingValidator.PostBuilding(new Building());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingBuilding()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.BuildingRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.BuildingRepository.Update(It.IsAny<Building>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IBuildingValidator buildingValidator = new BuildingValidator(mockUnitOfWork.Object);

            //act
            buildingValidator.PutBuilding("0", new Building() { });

            //Assert
            //Retorna exception
        }

     
    }
}
