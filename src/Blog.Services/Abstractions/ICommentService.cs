using Blog.Bases.Services;
using Blog.Entities.Comments;
using Blog.Services.Entities;

namespace Blog.Services.Abstractions
{
    public interface ICommentService
    {
        Task<ServiceOutput<CommentOutput>> CreateCommentAsync(ServiceInput<CommentInput> input);
        Task<ServiceOutput<CommentOutput>> UpdateCommentAsync(ServiceInput<CommentInput> input);
        Task<ServiceOutput<bool>> RemoveCommentAsync(Guid id);
    }
}
