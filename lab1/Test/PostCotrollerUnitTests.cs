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
    public class PostCotrollerUnitTests
    {
        private readonly Mock<ILogger<PostController>> _loggerMock = new();
        private readonly DbStorage _context = new InMemoryDbContext();
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(["REST"]);
        }).CreateMapper();

        private readonly PostService _postService;
        private readonly PostController _postController;
        private TweetResponseTO _tweet = null!;

        public PostCotrollerUnitTests()
        {
            _postService = new PostService(_context, _mapper);
            _postController = new PostController(_postService, _loggerMock.Object, _mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var authorLoggerMock = new Mock<ILogger<AuthorController>>();
            var authorService = new AuthorService(_context, _mapper);
            var authorController = new AuthorController(authorService, authorLoggerMock.Object, _mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            var authorList = (authorController.Read().Value as List<AuthorResponseTO>)!;
            AuthorResponseTO author;
            if (authorList.Count == 0)
            {
                author = ((await authorController.Create(new(0, "postTestAuthorName", "password", "fname", "lname"))).Value
                    as AuthorResponseTO)!;
            }
            else
            {
                author = authorList[0];
            }

            var tweetLoggerMock = new Mock<ILogger<TweetController>>();
            var tweetService = new TweetService(_context, _mapper);
            var tweetController = new TweetController(tweetService, tweetLoggerMock.Object, _mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            var tweetList = (tweetController.Read().Value as List<TweetResponseTO>)!;
            if (tweetList.Count == 0)
            {
                _tweet = ((await tweetController.Create(new(0, author!.Id, "postTestTitle", "content", DateTime.Now,
                    DateTime.Now))).Value as TweetResponseTO)!;
            }
            else
            {
                _tweet = tweetList[0];
            }

            _context.ChangeTracker.Clear();
        }

        [TestMethod]
        public void GetAll()
        {
            var result = _postController.Read();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<PostResponseTO>));
        }

        [TestMethod]
        public async Task Delete()
        {
            var postRequest = new PostRequestTO(0, _tweet.Id, "testPostDeleteContent");
            var postResponse = (await _postController.Create(postRequest)).Value as PostResponseTO;
            Assert.IsNotNull(postResponse);

            _context.ChangeTracker.Clear();

            var result = await _postController.Delete(postResponse.Id);

            var expected = typeof(NoContentResult);

            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public async Task Create()
        {
            var expectedName = "testCreateContent";

            var result = (await _postController.Create(new(0, _tweet.Id, expectedName))).Value
                as PostResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expectedName, result.Content);
        }

        [TestMethod]
        public async Task Update()
        {
            var expected = "testedUpdatePostContent";
            var postResponse = (await _postController.Create(new(0, _tweet.Id, "testUpdatePsotContent"))).Value
                as PostResponseTO;
            Assert.IsNotNull(postResponse);

            _context.ChangeTracker.Clear();

            var result =
                (await _postController.Update(new(postResponse.Id, _tweet.Id, expected))).Value
                as PostResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task Patch()
        {
            var expected = "newPatchPostTestContent";
            var postResponse =
                (await _postController.Create(new(0, _tweet.Id, expected))).Value
                as PostResponseTO;
            Assert.IsNotNull(postResponse);

            var patch = new JsonPatchDocument<Post>();
            var addOperation = new Operation<Post>
            {
                op = "add",
                path = "/Content",
                value = expected
            };
            patch.Operations.Add(addOperation);

            _context.ChangeTracker.Clear();

            var result = (await _postController.PartialUpdate(postResponse.Id, patch)).Value as PostResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result.Content);
        }
    }
}
