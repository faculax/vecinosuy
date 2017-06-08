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
    public class VoteValidatorTest
    {
        [TestMethod]
        public void GetAllVotesFromRepositoryTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.VoteRepository.Get(null, null, ""));

            VoteValidator VoteValidator = new VoteValidator(mockUnitOfWork.Object);

            ////Act
            IEnumerable<Vote> returnedVote = VoteValidator.GetVotes();

            ////Assert
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void GetVoteById()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al metodo Get del VoteRepository con un int
            mockUnitOfWork.Setup(un => un.VoteRepository.GetByID(It.IsAny<int>()));

            VoteValidator VoteValidator = new VoteValidator(mockUnitOfWork.Object);
            //Act

            Vote returnedVote = VoteValidator.GetVotesById(5);

            //Assert
            //mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void CreateVoteTest()
        {
            //Arrange
            //Creo el mock object del unitOfWork
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            //Esperamos que se llame al método Insert del Repository con un Vote y luego al Save();
            mockUnitOfWork.Setup(un => un.VoteRepository.Insert(It.IsAny<Vote>()));
            mockUnitOfWork.Setup(un => un.Save());

            VoteValidator VoteValidator = new VoteValidator(mockUnitOfWork.Object);

            //Act
            VoteValidator.PostVote(new Vote());

            //Assert
            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        [ExpectedException(typeof(NotExistException))]
        public void DoesntUpdateNonExistingVote()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(un => un.VoteRepository.GetByID(It.IsAny<int>())).Returns(() => null);

            mockUnitOfWork.Setup(un => un.VoteRepository.Update(It.IsAny<Vote>()));
            //mockUnitOfWork.Setup(un => un.Save());

            IVoteValidator VoteValidator = new VoteValidator(mockUnitOfWork.Object);

            //act
            VoteValidator.PutVote(0, new Vote() { });

            //Assert
            //Retorna exception
        }

        [TestMethod]
        public void UpdatesExistingBooking()
        {
            Vote Vote = new Vote
            {
                VoteId = 1,
                EndDate = new DateTime(2017, 10, 10),
                YesNoQuestion = "Questions?", 
                Deleted = false,
                Yes = 1,
                No = 2
            };
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
    .Setup(un => un.VoteRepository.GetByID(It.IsAny<int>()))
    .Returns(Vote);

            //Además, seteamos las expectativas para los métodos que deben llamarse luego
            mockUnitOfWork.Setup(un => un.VoteRepository.Update(It.IsAny<Vote>()));
            mockUnitOfWork.Setup(un => un.Save());

            VoteValidator VoteValidator = new VoteValidator(mockUnitOfWork.Object);

            //act
            VoteValidator.PutVote(1, Vote);

            //Assert
            mockUnitOfWork.Verify(un => un.VoteRepository.Update(It.IsAny<Vote>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
        }
    }
}
