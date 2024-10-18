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

        [HttpPost]
        public async Task<ServiceOutput<CommentOutput>> CreateCommentAsync(ServiceInput<CommentInput> input)
        {
            return await _commentService.CreateCommentAsync(input);
        }

        [HttpPut]
        public async Task<ServiceOutput<CommentOutput>> UpdateCommentAsync(ServiceInput<CommentInput> input)
        {
            return await _commentService.UpdateCommentAsync(input);
        }

        [HttpDelete]
        public async Task<ServiceOutput<bool>> RemoveCommentAsync(Guid id)
        {
            return await _commentService.RemoveCommentAsync(id);
        }
    }
}
