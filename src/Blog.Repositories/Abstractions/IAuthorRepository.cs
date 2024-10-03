using Blog.Entities.Authors;
using Blog.Repositories.Entities;

namespace Blog.Repositories.Abstractions
{
    public interface IAuthorRepository
    {
        Task<RepositoryOutput<Author>> GetAuthorByIdAsync(Guid id);
        Task<RepositoryOutput<Author>> CreateAuthorAsync(RepositoryInput<Author> input);
        Task<RepositoryOutput<Author>> UpdateAuthorAsync(RepositoryInput<Author> input);
        Task<RepositoryOutput<bool>> RemoveAuthorAsync(Guid id);
    }
}
