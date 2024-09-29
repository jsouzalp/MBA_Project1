using Blog.Entities.Authors;
using Blog.Repositories.Entities;

namespace Blog.Repositories.Abstractions
{
    public interface IAuthorRepository
    {
        Task<RepositoryOutput<Author>> InsertAuthorAsync(RepositoryInput<Author> input);
        Task<RepositoryOutput<Author>> UpdateAuthorAsync(RepositoryInput<Author> input);
        Task<RepositoryOutput<Author>> RemoveAuthorAsync(RepositoryInput<Author> input);
    }
}
