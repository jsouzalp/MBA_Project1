using Blog.Bases.Services;
using Blog.Entities.Posts;
using Blog.Services.Entities;

namespace Blog.Services.Abstractions
{
    public interface IPostService
    {
        Task<ServiceOutput<IEnumerable<PostOutput>>> FilterPostsAsync(FilterPostInput input);
        Task<ServiceOutput<PostOutput>> GetPostAsync(Guid id);
        Task<ServiceOutput<PostOutput>> CreatePostAsync(ServiceInput<PostInput> input);
        Task<ServiceOutput<PostOutput>> UpdatePostAsync(ServiceInput<PostInput> input);
        Task<ServiceOutput<bool>> RemovePostAsync(Guid id);
    }
}
