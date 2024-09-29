using Blog.Entities.Authors;
using Blog.Entities.Comments;

namespace Blog.Entities.Posts
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime Date {get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Author Author { get; set; }
    }
}
