namespace Blog.Entities.Comments
{
    public class CommentInput
    {
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ComentAuthorId { get; set; }
        public string Message { get; set; }
    }
}
