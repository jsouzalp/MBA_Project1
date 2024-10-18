using Blog.Entities.Authors;

namespace Blog.Repositories.Extensions
{
    public static class AuthorExtension
    {
        public static Author FillKeys(this Author author)
        {
            if (author.Id == Guid.Empty) { author.Id = Guid.NewGuid(); }
            if (author.Posts != null)
            {
                foreach (var post in author.Posts)
                {
                    post.AuthorId = author.Id;
                    if (post.Date == DateTime.MinValue) { post.Date = DateTime.Now; }
                    if (post.Id == Guid.Empty) { post.Id = Guid.NewGuid(); }

                    if (post.Comments != null)
                    {
                        foreach (var comment in post.Comments)
                        {
                            comment.PostId = post.Id;
                            if (comment.Id == Guid.Empty) { comment.Id = Guid.NewGuid(); }
                            if (comment.Date == DateTime.MinValue) { comment.Date = DateTime.Now; }
                        }
                    }
                }
            }

            return author;
        }
    }
}
