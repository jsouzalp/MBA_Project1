using Blog.Entities.Posts;

namespace Blog.Entities.Authors
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
