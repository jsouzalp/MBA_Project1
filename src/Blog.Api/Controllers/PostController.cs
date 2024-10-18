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

        [HttpGet]
        public async Task<ServiceOutput<IEnumerable<PostOutput>>> FilterPostsAsync([FromQuery] FilterPostInput input)
        {
            return await _postService.FilterPostsAsync(input);
        }

        [HttpPost]
        public async Task<ServiceOutput<PostOutput>> CreatePostAsync([FromBody] ServiceInput<PostInput> input)
        {
            return await _postService.CreatePostAsync(input);
        }

        [HttpPut]
        public async Task<ServiceOutput<PostOutput>> UpdatePostAsync([FromBody] ServiceInput<PostInput> input)
        {
            return await _postService.UpdatePostAsync(input);
        }

        [HttpDelete]
        public async Task<ServiceOutput<bool>> RemovePostAsync(Guid id)
        {
            return await _postService.RemovePostAsync(id);
        }
    }
}
