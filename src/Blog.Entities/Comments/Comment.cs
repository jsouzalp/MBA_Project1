using Blog.Entities.Posts;

namespace Blog.Entities.Comments
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentAuthorId { get; set; }
        public DateTime Date {get; set; }
        public string Message { get; set; }

        public Post Post { get; set; }
    }
}
