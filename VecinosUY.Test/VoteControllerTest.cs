using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VecinosUY.Data.Entities;
using VecinosUY.Exceptions;
using VecinosUY.Logic;
using Moq;
using VecinosUY.Data.Repository;
using VecinosUY.Web.Api.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;
using VecinosUY.Factory;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace VecinosUY.Test
{
    [TestClass]
    public class VoteControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetVotes()
        {
            var allVotes = new[]
            {
                new Vote()
                {
                    VoteId = 1,
                    EndDate = DateTime.Now.AddDays(2),
                    YesNoQuestion = "1",
                    Deleted = false,
                    Yes = 2, 
                    No = 1               
                },
               new Vote()
                {
                    VoteId = 2,
                    EndDate = DateTime.Now.AddDays(2),
                    YesNoQuestion = "2",
                    Deleted = false,
                    Yes = 4,
                    No = 2
                },
            };

            var mockVoteValidator = new Mock<IVoteValidator>();
            mockVoteValidator.Setup(x => x.GetVotes()).Returns(allVotes);

            var controller = new VotesController(mockVoteValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetVotes();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Vote>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void TestPostVote()
        {
            Vote a = new Vote()
            {
                VoteId = 1,
                EndDate = DateTime.Now.AddDays(2),
                YesNoQuestion = "1",
                Deleted = false,
                Yes = 2,
                No = 1
            };

            var mockVoteValidator = new Mock<IVoteValidator>();
            mockVoteValidator.Setup(x => x.PostVote(a)).Verifiable();
            mockVoteValidator.Setup(x => x.secure(null)).Verifiable();
            mockVoteValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new VotesController(mockVoteValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostVote(a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Vote>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteVote()
        {
            Vote a = new Vote()
            {
                VoteId = 1,
                EndDate = DateTime.Now.AddDays(2),
                YesNoQuestion = "1",
                Deleted = false,
                Yes = 2,
                No = 1
            };

            var mockVoteValidator = new Mock<IVoteValidator>();
            mockVoteValidator.Setup(x => x.DeleteVote(1)).Verifiable();
            mockVoteValidator.Setup(x => x.secure(null)).Verifiable();
            mockVoteValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new VotesController(mockVoteValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteVote(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Vote>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
