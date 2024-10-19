using Blog.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Blog.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController(){ }

        internal ActionResult<ServiceOutput<T>> GenerateResponse<T>(ServiceOutput<T> result,
            int responseStatus,
            [CallerMemberName] string caller = "")
        {
            if (!result.Success && result.Errors != null)
            {
                return BadRequest(result);
            }
            else if (result.Success && result.Output == null)
            {
                return NotFound(result);
            }
            else
            {
                switch (responseStatus)
                {
                    case StatusCodes.Status201Created:
                        return CreatedAtAction(caller, result);
                    case StatusCodes.Status204NoContent:
                    case StatusCodes.Status200OK:
                    default:
                        return Ok(result);
                }
            }
        }
    }
}
