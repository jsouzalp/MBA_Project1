using Blog.Bases;
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
    public partial class PostRepository : IPostRepository
    {
        // TODO :: Colocar o tratamento de exception em um extension

        private readonly BlogDbContext _context;
        private readonly ITranslationResource _translateResource;

        public PostRepository(BlogDbContext context, ITranslationResource translateResource)
        {
            _context = context;
            _translateResource = translateResource;
        }

        public async Task<RepositoryOutput<Post>> FilterPostsAsync(RepositoryInput<FilterPostInput> input)
        {
            RepositoryOutput<Post> result = new();
            try
            {
                result.Output = await _context.Posts
                    .Include(x => x.Comments).ThenInclude(x => x.CommentAuthor)
                    .Skip(input.Input.Skip)
                    .Take(input.Input.RecordsPerPage)
                    .FirstOrDefaultAsync(x => x.AuthorId == input.Input.AuthorId);
                if (result.Output != null)
                {
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryFilterSuccess), input.Input.AuthorId, input.Input.Page);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryFilterNotFound), input.Input.AuthorId, input.Input.Page);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(PostConstant.RepositoryFilterPostError),
                        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryFilterPostError), input?.Input?.AuthorId),
                        InternalMessage = ex.ToString()
                    }
                };
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(PostConstant.RepositoryCreatePostError),
                        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryCreatePostError), input.Input.Id),
                        InternalMessage = ex.ToString()
                    }
                };
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(PostConstant.RepositoryUpdatePostError),
                        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryUpdatePostError), input.Input.Id),
                        InternalMessage = ex.ToString()
                    }
                };
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
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        _ = await _context.Comments.Where(x => x.Id == post.Id).ExecuteDeleteAsync();
                        _ = _context.Posts.Remove(post);
                        _ = await _context.SaveChangesAsync();

                        transaction.Complete();
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(PostConstant.RepositoryRemovePostError),
                        Message = string.Format(_translateResource.GetResource(PostConstant.RepositoryRemovePostError), id),
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }
    }
}
