using Blog.Bases.Services;
using Blog.Entities.Posts;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Area("post")]
    [Route("api/v1/[area]")]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService) : base()
        {
            _postService = postService;
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<IEnumerable<PostOutput>>> FilterPostsAsync([FromQuery] FilterPostInput input)
        {
            return await _postService.FilterPostsAsync(input);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<PostOutput>> GetPostAsync(Guid id)
        {
            return await _postService.GetPostAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<PostOutput>> CreatePostAsync([FromBody] ServiceInput<PostInput> input)
        {
            return await _postService.CreatePostAsync(input);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<PostOutput>> UpdatePostAsync([FromBody] ServiceInput<PostInput> input)
        {
            return await _postService.UpdatePostAsync(input);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<bool>> RemovePostAsync(Guid id)
        {
            return await _postService.RemovePostAsync(id);
        }
    }
}
