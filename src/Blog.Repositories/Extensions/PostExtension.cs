using Blog.Entities.Posts;

namespace Blog.Repositories.Extensions
{
    public static class PostExtension
    {
        public static Post FillKeys(this Post post)
        {
            if (post.Id == Guid.Empty) { post.Id = Guid.NewGuid(); }
            if (post.Date == DateTime.MinValue) { post.Date = DateTime.Now; }
            if (post.Comments != null)
            {
                foreach (var comment in post.Comments)
                {
                    comment.PostId = post.Id;
                    if (comment.Id == Guid.Empty) { comment.Id = Guid.NewGuid(); }
                    if (comment.Date == DateTime.MinValue) { comment.Date = DateTime.Now; }
                }
            }

            return post;
        }
    }
}
