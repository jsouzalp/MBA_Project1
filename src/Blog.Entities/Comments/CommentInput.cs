namespace Blog.Entities.Comments
{
    public class CommentInput
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentAuthorId { get; set; }
        public string Message { get; set; }
    }
}
