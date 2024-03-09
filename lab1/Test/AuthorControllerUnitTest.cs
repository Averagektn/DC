using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using REST.Controllers.V1_0;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Implementation;
using REST.Service.Interface;
using REST.Storage.InMemoryDb;
using System.Net;

namespace Test
{
    [TestClass]
    public class AuthorControllerUnitTest
    {
        private static readonly string[] assemblyNamesToScan = ["REST"];

        [TestMethod]
        public async Task GetByTweetID_ReturnsJsonResult()
        {
            int tweetId = 1;
            var expectedResponse = new AuthorResponseTO(1, "login", "fname", "lname");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assemblyNamesToScan);
            });

            var mapper = config.CreateMapper();

            var storage = new InMemoryDbContext();

            var authorService = new AuthorService(storage, mapper);
            var tweetService = new TweetService(storage, mapper);

            var authorController = new AuthorController(authorService, new Mock<ILogger<AuthorController>>().Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            var tweetController = new TweetController(tweetService, new Mock<ILogger<TweetController>>().Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            await authorController.Create(new("login", "password", "fname", "lname"));
            await tweetController.Create(new(1, "title", "content", DateTime.Now, DateTime.Now));

            var result = await authorController.GetByTweetID(tweetId);

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

        private static Mock<IMapper> GetMapper()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Author>(It.IsAny<AuthorRequestTO>()))
                .Returns((AuthorRequestTO source) => new Author(1, "login", "password", "fname", "lname"));
            mockMapper.Setup(x => x.Map<AuthorResponseTO>(It.IsAny<Author>()))
                .Returns((Author source) => new(1, "login", "fname", "lname"));
            mockMapper.Setup(x => x.Map<Tweet>(It.IsAny<TweetRequestTO>()))
                .Returns((TweetRequestTO tweet) => new(1, new(1, "login", "password", "fname", "lname"), "title", "content", DateTime.Now, DateTime.Now));

            return mockMapper;
        }
    }
}