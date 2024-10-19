using Blog.Entities.Posts;

namespace Blog.Entities.Authors
{
    public class AuthorInput
    {
        public Guid Id { get; set; }
        public Guid IdentityUser { get; set; }
        public string Name { get; set; }
        public ICollection<PostInput> Posts { get; set; }
    }
}
