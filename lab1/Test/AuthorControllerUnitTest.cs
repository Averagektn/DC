using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using REST.Controllers.V1_0;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using System.Net;

namespace Test
{
    [TestClass]
    public class AuthorControllerUnitTest
    {
        [TestMethod]
        public async Task GetByTweetID_ReturnsJsonResult()
        {
            int tweetId = 1;
            var expectedResponse = new AuthorResponseTO(1, "login", "fname", "lname");

            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.GetByTweetID(tweetId))
                .ReturnsAsync(expectedResponse);

            var loggerMock = new Mock<ILogger<AuthorController>>();

            var controller = new AuthorController(authorServiceMock.Object, loggerMock.Object);

            var result = await controller.GetByTweetID(tweetId);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [TestMethod]
        public async Task GetByTweetID_ReturnsBadRequest_WhenExceptionThrown()
        {
            int tweetId = 1;
            var expectedException = new Exception("Some error message");

            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.GetByTweetID(tweetId))
                .ThrowsAsync(expectedException);

            var loggerMock = new Mock<ILogger<AuthorController>>();

            var controller = new AuthorController(authorServiceMock.Object, loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = await controller.GetByTweetID(tweetId);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, controller.Response.StatusCode);
        }
    }
}