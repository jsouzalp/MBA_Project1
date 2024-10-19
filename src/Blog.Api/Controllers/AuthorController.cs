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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<AuthorOutput>> GetAuthorByIdAsync(Guid id)
        {
            return await _authorService.GetAuthorByIdAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<AuthorOutput>> CreateAuthorAsync([FromBody] ServiceInput<AuthorInput> input)
        {
            return await _authorService.CreateAuthorAsync(input);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<AuthorOutput>> UpdateAuthorAsync([FromBody] ServiceInput<AuthorInput> input)
        {
            return await _authorService.UpdateAuthorAsync(input);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ServiceOutput<bool>> RemoveAuthorAsync(Guid id)
        {
            return await _authorService.RemoveAuthorAsync(id);
        }
    }
}
