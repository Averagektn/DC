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
        private readonly string[] assemblyNamesToScan = ["REST"];
        private Mock<ILogger<AuthorController>> _loggerMock = null!;
        private DbStorage _context = null!;
        private IMapper _mapper = null!;

        [TestInitialize]
        public void TestInit()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assemblyNamesToScan);
            });
            _mapper = config.CreateMapper();
            _loggerMock = new Mock<ILogger<AuthorController>>();
            _context = new InMemoryDbContext();
        }

        [TestMethod]
        public void GetAll()
        {
            var authorService = new AuthorService(_context, _mapper);
            var authorController = new AuthorController(authorService, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = authorController.Read();
            var expected = 1;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<AuthorResponseTO>));
            Assert.AreEqual(expected, ((List<AuthorResponseTO>)result.Value).Count);
        }

        [TestMethod]
        public async Task GetByTweetID()
        {
            int tweetId = 1;
            var author = new Author(1, "login", "password", "fname", "lname");
            var authorRequest = new AuthorRequestTO(0, author.Login, author.Password, author.FirstName, author.LastName);
            var authorResponse = new AuthorResponseTO(author.Id, author.Login, author.FirstName, author.LastName);
            var tweetRequest = new TweetRequestTO(0, 1, "title", "content", DateTime.Now, DateTime.Now);

            var authorService = new AuthorService(_context, _mapper);
            var tweetService = new TweetService(_context, _mapper);
            var authorController = new AuthorController(authorService, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            var tweetController = new TweetController(tweetService, new Mock<ILogger<TweetController>>().Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            await authorController.Create(authorRequest);
            await tweetController.Create(tweetRequest);

            var result = await authorController.GetByTweetID(tweetId);

            Assert.IsNotNull(result);
            Assert.AreEqual(authorResponse, result.Value);
        }

        [TestMethod]
        public async Task Delete()
        {
            var authorService = new AuthorService(_context, _mapper);
            var authorController = new AuthorController(authorService, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            await authorController.Create(new(0, "login", "password", "fname", "lname"));

            _context.ChangeTracker.Clear();
            
            var result = await authorController.Delete(1);

            var expected = typeof(NoContentResult);

            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public async Task Create()
        {
            var authorService = new AuthorService(_context, _mapper);
            var authorController = new AuthorController(authorService, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = await authorController.Create(new(0, "login", "password", "fname", "lname"));
            var expected = new AuthorResponseTO(2, "login", "fname", "lname");

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public async Task Update()
        {
            var authorService = new AuthorService(_context, _mapper);
            var authorController = new AuthorController(authorService, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            await authorController.Create(new(0, "login", "password", "fname", "lname"));

            _context.ChangeTracker.Clear();

            var expected = new AuthorResponseTO(1, "newLogin", "fname", "lname");
            var result = await authorController.Update(new(1, "newLogin", "password", "fname", "lname"));

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public async Task APatch()
        {
            var authorService = new AuthorService(_context, _mapper);
            var authorController = new AuthorController(authorService, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            await authorController.Create(new(0, "login", "password", "fname", "lname"));

            var expected = new AuthorResponseTO(2, "login", "newName", "lname");
            var patch = new JsonPatchDocument<Author>();
            var addOperation = new Operation<Author>
            {
                op = "add",
                path = "/FirstName",
                value = "newName"
            };
            patch.Operations.Add(addOperation);

            _context.ChangeTracker.Clear();

            var result = await authorController.PartialUpdate(2, patch);

            Assert.AreEqual(expected, result.Value);
        }
    }
}