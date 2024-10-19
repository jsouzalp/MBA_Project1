using Blog.Bases.Services;
using Blog.Entities.Comments;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Area("comment")]
    [Route("api/v1/[area]")]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) : base()
        {
            _commentService = commentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<CommentOutput>>> GetCommentAsync(Guid id)
        {
            var result = await _commentService.GetCommentAsync(id);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<CommentOutput>>> CreateCommentAsync([FromBody] ServiceInput<CommentInput> input)
        {
            var result = await _commentService.CreateCommentAsync(input);
            return GenerateResponse(result, StatusCodes.Status201Created);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<CommentOutput>>> UpdateCommentAsync([FromBody] ServiceInput<CommentInput> input)
        {
            var result = await _commentService.UpdateCommentAsync(input);
            return GenerateResponse(result, StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<bool>>> RemoveCommentAsync(Guid id)
        {
            var result = await _commentService.RemoveCommentAsync(id);
            return GenerateResponse(result, StatusCodes.Status204NoContent);
        }
    }
}
