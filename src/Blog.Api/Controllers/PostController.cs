﻿using Blog.Bases.Services;
using Blog.Entities.Posts;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<IEnumerable<PostOutput>>>> FilterPostsAsync([FromQuery] FilterPostInput input)
        {
            var result = await _postService.FilterPostsAsync(input);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<PostOutput>>> GetPostAsync(Guid id)
        {
            var result = await _postService.GetPostAsync(id);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<PostOutput>>> CreatePostAsync([FromBody] ServiceInput<PostInput> input)
        {
            var result = await _postService.CreatePostAsync(input);
            return GenerateResponse(result, StatusCodes.Status201Created);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<PostOutput>>> UpdatePostAsync([FromBody] ServiceInput<PostInput> input)
        {
            var result = await _postService.UpdatePostAsync(input);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<bool>>> RemovePostAsync(Guid id)
        {
            var result = await _postService.RemovePostAsync(id);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }
    }
}
