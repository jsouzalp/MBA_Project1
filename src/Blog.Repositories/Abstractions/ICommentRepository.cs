using Blog.Entities.Comments;
using Blog.Repositories.Entities;

namespace Blog.Repositories.Abstractions
{
    public interface ICommentRepository
    {
        Task<RepositoryOutput<Comment>> CreateCommentAsync(RepositoryInput<Comment> input);
        Task<RepositoryOutput<Comment>> UpdateCommentAsync(RepositoryInput<Comment> input);
        Task<RepositoryOutput<bool>> RemoveCommentAsync(Guid id);
    }
}
