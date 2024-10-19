using Blog.Entities.Authors;
using Blog.Entities.Comments;
using System.Text.Json.Serialization;

namespace Blog.Entities.Posts
{
    public class Post
    {
        #region Attributes
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get => Author?.Name ?? "Autor não localizado"; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public ICollection<Comment> Comments { get; set; }
        #endregion

        #region Helper only for EF Mapping
        [JsonIgnore]
        public Author Author { get; set; }
        #endregion
    }
}
