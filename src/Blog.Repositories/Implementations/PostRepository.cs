using Blog.Bases;
using Blog.Entities.Comments;
using Blog.Entities.Posts;
using Blog.Repositories.Abstractions;
using Blog.Repositories.Contexts;
using Blog.Repositories.Entities;
using Blog.Repositories.Extensions;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Blog.Repositories.Implementations
{
    public partial class PostRepository : RepositoryBase, IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context, ITranslationResource translateResource)
            : base(translateResource)
        {
            _context = context;
        }

        public async Task<RepositoryOutput<Post>> GetInternalPostAsync(Guid id)
        {
            RepositoryOutput<Post> result = new();
            try
            {
                result.Output = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

                if (result.Output != null)
                {
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostSelect), id);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, PostConstant.RepositorySelectError, new object[] { id });

                //result.Errors = new List<ErrorBase>()
                //{
                //    new ErrorBase()
                //    {
                //        Code = _translateResource.GetCodeResource(PostConstant.RepositorySelectError),
                //        Message = string.Format(_translateResource.GetResource(PostConstant.RepositorySelectError), id),
                //        InternalMessage = ex.ToString()
                //    }
                //};
            }

            return result;
        }

        public async Task<RepositoryOutput<IEnumerable<Post>>> FilterPostsAsync(RepositoryInput<FilterPostInput> input)
        {
            RepositoryOutput<IEnumerable<Post>> result = new();
            try
            {
                result.Output = await _context.Posts
                    .Where(x => input.Input.AuthorId != null ? x.AuthorId == input.Input.AuthorId : 1 == 1)
                    .Include(x => x.Author)
                    .Include(x => x.Comments).ThenInclude(x => x.CommentAuthor)
                    .Skip(input.Input.Skip)
                    .Take(input.Input.RecordsPerPage)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();
                if (result.Output != null)
                {
                    foreach (Post post in result.Output)
                    {
                        post.Comments = post.Comments
                            .OrderByDescending(x => x.Date)
                            .ToList();
                    }
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryFilterSuccess), input.Input.AuthorId, input.Input.Page);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryFilterNotFound), input.Input.AuthorId, input.Input.Page);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, PostConstant.RepositoryFilterPostError, new object[] { input?.Input?.AuthorId });

                //result.Errors = new List<ErrorBase>()
                //{
                //    new ErrorBase()
                //    {
                //        Code = _translateResource.GetCodeResource(PostConstant.RepositoryFilterPostError),
                //        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryFilterPostError), input?.Input?.AuthorId),
                //        InternalMessage = ex.ToString()
                //    }
                //};
            }

            return result;
        }

        public async Task<RepositoryOutput<Post>> GetPostAsync(Guid id)
        {
            RepositoryOutput<Post> result = new();
            try
            {
                result.Output = await _context.Posts
                    .Include(x => x.Author)
                    .Include(x => x.Comments).ThenInclude(x => x.CommentAuthor)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (result.Output != null)
                {
                    result.Output.Comments = result.Output.Comments
                        .OrderByDescending(x => x.Date)
                        .ToList();
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostSelect), id);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, PostConstant.RepositorySelectError, new object[] { id });

                //result.Errors = new List<ErrorBase>()
                //{
                //    new ErrorBase()
                //    {
                //        Code = _translateResource.GetCodeResource(PostConstant.RepositorySelectError),
                //        Message = string.Format(_translateResource.GetResource(PostConstant.RepositorySelectError), id),
                //        InternalMessage = ex.ToString()
                //    }
                //};
            }

            return result;
        }

        public async Task<RepositoryOutput<Post>> CreatePostAsync(RepositoryInput<Post> input)
        {
            RepositoryOutput<Post> result = new();
            try
            {
                input.Input.FillKeys();
                await _context.Posts.AddAsync(input.Input);
                _ = await _context.SaveChangesAsync();
                result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostCreated), input.Input.Id);
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, PostConstant.RepositoryCreatePostError, new object[] { input.Input.Id });

                //result.Errors = new List<ErrorBase>()
                //{
                //    new ErrorBase()
                //    {
                //        Code = _translateResource.GetCodeResource(PostConstant.RepositoryCreatePostError),
                //        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryCreatePostError), input.Input.Id),
                //        InternalMessage = ex.ToString()
                //    }
                //};
            }

            return result;
        }

        public async Task<RepositoryOutput<Post>> UpdatePostAsync(RepositoryInput<Post> input)
        {
            RepositoryOutput<Post> result = new();
            try
            {
                Post post = await _context.Posts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == input.Input.Id);

                if (post != null)
                {
                    input.Input.FillKeys();
                    _context.Posts.Update(input.Input);
                    _ = await _context.SaveChangesAsync();
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostUpdated), input.Input.Id);
                    result.Output = input.Input;
                }
                else
                {
                    result.Output = input.Input;
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostNotFound), input.Input.Id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, PostConstant.RepositoryUpdatePostError, new object[] { input.Input.Id });

                //result.Errors = new List<ErrorBase>()
                //{
                //    new ErrorBase()
                //    {
                //        Code = _translateResource.GetCodeResource(PostConstant.RepositoryUpdatePostError),
                //        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryUpdatePostError), input.Input.Id),
                //        InternalMessage = ex.ToString()
                //    }
                //};
            }

            return result;
        }

        public async Task<RepositoryOutput<bool>> RemovePostAsync(Guid id)
        {
            RepositoryOutput<bool> result = new();
            try
            {
                Post post = await _context.Posts
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (post != null)
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        foreach (Comment comment in post.Comments)
                        {
                            _context.Comments.Remove(comment);
                        }
                        _ = _context.Posts.Remove(post);
                        _ = await _context.SaveChangesAsync();

                        //transaction.Complete();
                        await transaction.CommitAsync();
                        result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostRemoved), post.Id);
                        result.Output = true;
                    }
                }
                else
                {
                    result.Output = false;
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryPostNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, PostConstant.RepositoryRemovePostError, new object[] { id });

                //result.Errors = new List<ErrorBase>()
                //{
                //    new ErrorBase()
                //    {
                //        Code = _translateResource.GetCodeResource(PostConstant.RepositoryRemovePostError),
                //        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryRemovePostError), id),
                //        InternalMessage = ex.ToString()
                //    }
                //};
            }

            return result;
        }
    }
}
