using Blog.Bases.Services;
using Blog.Entities.Comments;
using Blog.Entities.Posts;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<CommentOutput>> GetCommentAsync(Guid id)
        {
            return await _commentService.GetCommentAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<CommentOutput>> CreateCommentAsync([FromBody] ServiceInput<CommentInput> input)
        {
            return await _commentService.CreateCommentAsync(input);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<CommentOutput>> UpdateCommentAsync([FromBody] ServiceInput<CommentInput> input)
        {
            return await _commentService.UpdateCommentAsync(input);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<bool>> RemoveCommentAsync(Guid id)
        {
            return await _commentService.RemoveCommentAsync(id);
        }
    }
}
