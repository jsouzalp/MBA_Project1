using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Area("comment")]
    [Route("api/v1/[area]")]
    public class CommentController : BaseController
    {
        public CommentController() : base()
        {
            
        }


    }
}
