using Blog.Entities.Comments;

namespace Blog.Repositories.Extensions
{
    public static class CommentExtension
    {
        public static Comment FillKeys(this Comment comment)
        {
            if (comment.Id == Guid.Empty) { comment.Id = Guid.NewGuid(); }
            if (comment.Date == DateTime.MinValue) { comment.Date = DateTime.Now; }
            return comment;
        }
    }
}
