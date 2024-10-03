using Blog.Entities.Comments;
using Blog.Entities.Posts;
using System.Text.Json.Serialization;

namespace Blog.Entities.Authors
{
    public class Author
    {
        #region Attributes
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        #endregion

        #region Helper only for EF Mapping
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
        #endregion
    }
}
