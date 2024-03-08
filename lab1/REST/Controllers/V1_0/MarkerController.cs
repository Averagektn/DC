using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Implementation;
using REST.Service.Interface;
using System.Net;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/markers")]
    [ApiController]
    public class MarkerController(ILogger<MarkerController> Logger, IMarkerService MarkerService) : Controller
    {
        [HttpGet]
        public JsonResult Read()
        {
            var authors = MarkerService.GetAll();

            Logger.LogInformation("Markers read");

            return Json(authors);
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody] MarkerRequestTO marker)
        {
            MarkerResponseTO? response = null;
            Logger.LogInformation("Creating {res}", Json(marker).Value);
            Response.StatusCode = (int)HttpStatusCode.Created;

            try
            {
                response = await MarkerService.Add(marker);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at ADD MARKER {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MarkerRequestTO marker)
        {
            MarkerResponseTO? response = null;
            Logger.LogInformation("Updating author: {marker}", Json(marker).Value);

            try
            {
                response = await MarkerService.Update(marker);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at UPDATE MARKER {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Marker> marker)
        {
            MarkerResponseTO? response = null;
            Logger.LogInformation("Patching {marker}", marker);

            try
            {
                response = await MarkerService.Patch(id, marker);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at PATCH MARKER {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool res = false;
            Logger.LogInformation("Deleted marker {id}", id);

            try
            {
                res = await MarkerService.Remove(id);
            }
            catch
            {
                Logger.LogInformation("Deleting failed {id}", id);
            }

            return res ? Ok() : BadRequest();
        }
    }
}
