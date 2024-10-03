using Blog.Entities.Comments;

namespace Blog.Entities.Posts
{
    public class PostInput
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
