using Blog.Entities.Comments;

namespace Blog.Entities.Posts
{
    public class PostOutput
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        //public int TotalComments { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }
    }
}
