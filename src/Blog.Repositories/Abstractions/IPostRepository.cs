using Blog.Entities.Posts;
using Blog.Repositories.Entities;

namespace Blog.Repositories.Abstractions
{
    public interface IPostRepository
    {
        Task<RepositoryOutput<IEnumerable<Post>>> FilterPostsAsync(RepositoryInput<FilterPostInput> input);
        Task<RepositoryOutput<Post>> CreatePostAsync(RepositoryInput<Post> input);
        Task<RepositoryOutput<Post>> UpdatePostAsync(RepositoryInput<Post> input);
        Task<RepositoryOutput<bool>> RemovePostAsync(Guid id);
    }
}
