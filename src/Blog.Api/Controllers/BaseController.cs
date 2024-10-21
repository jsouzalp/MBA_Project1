using Blog.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController(){ }

        internal ActionResult<ServiceOutput<T>> GenerateResponse<T>(ServiceOutput<T> result,
            int responseStatus)
        {
            if (!result.Success && result.Errors != null)
            {
                responseStatus = StatusCodes.Status400BadRequest;
            }
            else if (result.Success && result.Output == null)
            {
                responseStatus = StatusCodes.Status404NotFound;
            }

            return new JsonResult(result)
            {
                StatusCode = responseStatus
            };
        }
    }
}
