﻿using Microsoft.AspNetCore.Mvc;
using REST.Controllers.Common;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/markers")]
    [ApiController]
    public class MarkerController(IMarkerService MarkerService, ILogger<MarkerController> Logger) :
        AbstractController<Marker, MarkerRequestTO, MarkerResponseTO>(MarkerService, Logger)
    { }
}
