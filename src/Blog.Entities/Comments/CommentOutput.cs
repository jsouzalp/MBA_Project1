namespace Blog.Entities.Comments
{
    public class CommentOutput
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentAuthorId { get; set; }
        public DateTime Date { get; set; }
        public string CommentAuthorName { get; set; }
        public string Message { get; set; }
    }
}
