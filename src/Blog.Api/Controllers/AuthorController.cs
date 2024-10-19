using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Area("author")]
    [Route("api/v1/[area]")]
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService) : base()
        {
            _authorService = authorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<AuthorOutput>>> GetAuthorByIdAsync(Guid id)
        {
            var result = await _authorService.GetAuthorByIdAsync(id);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<AuthorOutput>>> CreateAuthorAsync([FromBody] ServiceInput<AuthorInput> input)
        {
            var result = await _authorService.CreateAuthorAsync(input);
            return GenerateResponse(result, StatusCodes.Status201Created);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<AuthorOutput>>> UpdateAuthorAsync([FromBody] ServiceInput<AuthorInput> input)
        {
            var result = await _authorService.UpdateAuthorAsync(input);
            return GenerateResponse(result, StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<bool>>> RemoveAuthorAsync(Guid id)
        {
            var result = await _authorService.RemoveAuthorAsync(id);
            return GenerateResponse(result, StatusCodes.Status204NoContent);
        }
    }
}
