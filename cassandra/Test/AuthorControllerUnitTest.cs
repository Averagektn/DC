using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using REST.Controllers.V1_0;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Implementation;
using REST.Storage.Common;
using REST.Storage.InMemoryDb;

namespace Test
{
    [TestClass]
    public class AuthorControllerUnitTest
    {
        private readonly Mock<ILogger<AuthorController>> _loggerMock = new();
        private readonly DbStorage _context = new InMemoryDbContext();
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(["REST"]);
        }).CreateMapper();

        private readonly AuthorService _authorService;
        private readonly AuthorController _authorController;

        public AuthorControllerUnitTest()
        {
            _authorService = new AuthorService(_context, _mapper);
            _authorController = new AuthorController(_authorService, _loggerMock.Object, _mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
        }

        [TestMethod]
        public void GetAll()
        {
            var result = _authorController.Read();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<AuthorResponseTO>));
        }

        [TestMethod]
        public async Task GetByTweetID()
        {
            var authorRequest = new AuthorRequestTO(0, "author", "passwrod", "fname", "lname");
            var authorResponse = (await _authorController.Create(authorRequest)).Value as AuthorResponseTO;
            Assert.IsNotNull(authorResponse);

            var tweetRequest = new TweetRequestTO(0, authorResponse.Id, "title", "content", DateTime.Now, DateTime.Now);

            var tweetService = new TweetService(_context, _mapper);
            var tweetController = new TweetController(tweetService, new Mock<ILogger<TweetController>>().Object, _mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var tweetResponse = (await tweetController.Create(tweetRequest)).Value as TweetResponseTO;
            Assert.IsNotNull(tweetResponse);

            var result = await _authorController.GetByTweetID(tweetResponse.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(authorResponse, result.Value);
        }

        [TestMethod]
        public async Task Delete()
        {
            var authorRequest = new AuthorRequestTO(0, "testDeleteLogin", "password", "fname", "lname");
            var authorResponse = (await _authorController.Create(authorRequest)).Value as AuthorResponseTO;
            Assert.IsNotNull(authorResponse);

            _context.ChangeTracker.Clear();

            var result = await _authorController.Delete(authorResponse.Id);

            var expected = typeof(NoContentResult);

            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public async Task Create()
        {
            var expectedLogin = "testCreateLogin";
            var result = (await _authorController.Create(new(0, expectedLogin, "password", "fname", "lname"))).Value
                as AuthorResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expectedLogin, result.Login);
        }

        [TestMethod]
        public async Task Update()
        {
            var expected = "testedUpdateLogin";
            var authorResponse = (await _authorController.Create(new(0, "testUpdateLogin", "password", "fname", "lname"))).Value
                as AuthorResponseTO;
            Assert.IsNotNull(authorResponse);

            _context.ChangeTracker.Clear();

            var result =
                (await _authorController.Update(new(authorResponse.Id, expected, "password", "fname", "lname"))).Value
                as AuthorResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result.Login);
        }

        [TestMethod]
        public async Task Patch()
        {
            var expected = "newPatchTestName";
            var authorResponse =
                (await _authorController.Create(new(0, "patchTestLogin", "password", "fname", "lname"))).Value
                as AuthorResponseTO;
            Assert.IsNotNull(authorResponse);

            var patch = new JsonPatchDocument<Author>();
            var addOperation = new Operation<Author>
            {
                op = "add",
                path = "/FirstName",
                value = expected
            };
            patch.Operations.Add(addOperation);

            _context.ChangeTracker.Clear();

            var result = (await _authorController.PartialUpdate(authorResponse.Id, patch)).Value as AuthorResponseTO;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.FirstName);
        }
    }
}