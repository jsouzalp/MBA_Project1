using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Services.Entities;

namespace Blog.Services.Abstractions
{
    public interface IAuthorService
    {
        Task<ServiceOutput<Author>> InsertAuthorAsync(ServiceInput<Author> input);
        Task<ServiceOutput<Author>> UpdateAuthorAsync(ServiceInput<Author> input);
        Task<ServiceOutput<Author>> RemoveAuthorAsync(ServiceInput<Author> input);
    }
}
