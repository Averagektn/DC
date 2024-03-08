using Microsoft.AspNetCore.Mvc;
using REST.Service.Interface.Common;

namespace REST.Controllers.Common
{
    public abstract class AbstractController<Entity, RequestTO, ResponseTO>
        (ICrudService<Entity, RequestTO, ResponseTO> service) : Controller where Entity : class
    {
    }
}
