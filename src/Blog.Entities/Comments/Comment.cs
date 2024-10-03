using Blog.Entities.Authors;
using Blog.Entities.Posts;
using System.Text.Json.Serialization;

namespace Blog.Entities.Comments
{
    public class Comment
    {
        #region Attributes
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentAuthorId { get; set; }
        public DateTime Date {get; set; }
        public string Message { get; set; }
        public Author CommentAuthor { get; set; }
        #endregion

        #region Helper only for EF Mapping
        [JsonIgnore]
        public Post Post { get; set; }
        #endregion
    }
}
