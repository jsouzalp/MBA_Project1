using Blog.Entities.Posts;

namespace Blog.Entities.Authors
{
    public class AuthorOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalPosts { get; set; }
        public DateTime? LastPostDate { get; set; }
        public ICollection<PostOutput> Posts { get; set; }
    }
}
