using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("/api/v1.0/markers")]
    public class MarkerController(ILogger<MarkerController> Logger, IMarkerService MarkerService) : Controller
    {
        [HttpGet]
        public JsonResult Read()
        {
            var authors = MarkerService.GetAll();

            Logger.LogInformation("Authors read");

            return Json(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MarkerRequestTO marker)
        {
            var res = MarkerService.Add(marker);

            Logger.LogInformation("Creating {res}", Json(marker).Value);

            return await res ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MarkerRequestTO marker)
        {
            var res = MarkerService.Update(marker);

            Logger.LogInformation("Updated author: {author}", Json(marker).Value);

            return await res ? Ok() : BadRequest();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Marker> marker)
        {
            var res = MarkerService.Patch(id, marker);

            Logger.LogInformation("Patched {author}", marker);

            return await res ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = MarkerService.Remove(id);

            Logger.LogInformation("Deleted {id}", id);

            return await res ? Ok() : BadRequest();
        }
    }
}
