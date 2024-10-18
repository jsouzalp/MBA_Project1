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
        public async Task<ServiceOutput<AuthorOutput>> GetAuthorByIdAsync(Guid id)
        {
            return await _authorService.GetAuthorByIdAsync(id);
        }

        [HttpPost]
        public async Task<ServiceOutput<AuthorOutput>> CreateAuthorAsync(ServiceInput<AuthorInput> input)
        {
            return await _authorService.CreateAuthorAsync(input);
        }

        [HttpPut]
        public async Task<ServiceOutput<AuthorOutput>> UpdateAuthorAsync(ServiceInput<AuthorInput> input)
        {
            return await _authorService.UpdateAuthorAsync(input);
        }

        [HttpDelete]
        public async Task<ServiceOutput<bool>> RemoveAuthorAsync(Guid id)
        {
            return await _authorService.RemoveAuthorAsync(id);
        }
    }
}
