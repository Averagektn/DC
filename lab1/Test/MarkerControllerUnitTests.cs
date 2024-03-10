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
    public class MarkerControllerUnitTests
    {
        private readonly Mock<ILogger<MarkerController>> _loggerMock = new();
        private readonly DbStorage _context = new InMemoryDbContext();
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(["REST"]);
        }).CreateMapper();

        private readonly MarkerService _markerService;
        private readonly MarkerController _markerController;

        public MarkerControllerUnitTests()
        {
            _markerService = new MarkerService(_context, _mapper);
            _markerController = new MarkerController(_markerService, _loggerMock.Object, _mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
        }

        [TestMethod]
        public void GetAll()
        {
            var result = _markerController.Read();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<MarkerResponseTO>));
        }

        [TestMethod]
        public async Task Delete()
        {
            var markerRequest = new MarkerRequestTO(0, "testMarkerDeleteName");
            var markerResponse = (await _markerController.Create(markerRequest)).Value as MarkerResponseTO;
            Assert.IsNotNull(markerResponse);

            _context.ChangeTracker.Clear();

            var result = await _markerController.Delete(markerResponse.Id);

            var expected = typeof(NoContentResult);

            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public async Task Create()
        {
            var expectedName = "testCreateName";

            var result = (await _markerController.Create(new(0, expectedName))).Value
                as MarkerResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expectedName, result.Name);
        }

        [TestMethod]
        public async Task Update()
        {
            var expected = "testedUpdateMarkerName";
            var markerResponse = (await _markerController.Create(new(0, "testUpdateMarkerName"))).Value
                as MarkerResponseTO;
            Assert.IsNotNull(markerResponse);

            _context.ChangeTracker.Clear();

            var result =
                (await _markerController.Update(new(markerResponse.Id, expected))).Value
                as MarkerResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result.Name);
        }

        [TestMethod]
        public async Task Patch()
        {
            var expected = "newPatchMarkerTestName";
            var markerResponse =
                (await _markerController.Create(new(0, expected))).Value
                as MarkerResponseTO;
            Assert.IsNotNull(markerResponse);

            var patch = new JsonPatchDocument<Marker>();
            var addOperation = new Operation<Marker>
            {
                op = "add",
                path = "/Name",
                value = expected
            };
            patch.Operations.Add(addOperation);

            _context.ChangeTracker.Clear();

            var result = (await _markerController.PartialUpdate(markerResponse.Id, patch)).Value as MarkerResponseTO;
            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result.Name);
        }
    }
}
