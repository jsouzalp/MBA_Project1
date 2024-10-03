using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Services.Entities;

namespace Blog.Services.Abstractions
{
    public interface IAuthorService
    {
        Task<ServiceOutput<AuthorOutput>> GetAuthorByIdAsync(Guid id);
        Task<ServiceOutput<AuthorOutput>> CreateAuthorAsync(ServiceInput<AuthorInput> input);
        Task<ServiceOutput<AuthorOutput>> UpdateAuthorAsync(ServiceInput<AuthorInput> input);
        Task<ServiceOutput<bool>> RemoveAuthorAsync(Guid id);
    }
}
