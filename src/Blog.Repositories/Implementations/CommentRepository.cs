using Blog.Entities.Comments;
using Blog.Repositories.Abstractions;
using Blog.Repositories.Contexts;
using Blog.Repositories.Entities;
using Blog.Repositories.Extensions;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories.Implementations
{
    public partial class CommentRepository : RepositoryBase, ICommentRepository
    {
        private readonly BlogDbContext _context;

        public CommentRepository(BlogDbContext context, ITranslationResource translateResource)
            : base(translateResource)
        {
            _context = context;
        }

        public async Task<RepositoryOutput<Comment>> GetInternalCommentAsync(Guid id)
        {
            RepositoryOutput<Comment> result = new();
            try
            {
                result.Output = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

                if (result.Output != null)
                {
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentSelect), id);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, CommentConstant.RepositorySelectCommentError, new object[] { id });
            }

            return result;
        }

        public async Task<RepositoryOutput<Comment>> GetCommentAsync(Guid id)
        {
            RepositoryOutput<Comment> result = new();
            try
            {
                result.Output = await _context.Comments
                    .Include(x => x.CommentAuthor)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                if (result.Output != null)
                {
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentSelect), id);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, CommentConstant.RepositorySelectCommentError, new object[] { id });
            }

            return result;
        }

        public async Task<RepositoryOutput<Comment>> CreateCommentAsync(RepositoryInput<Comment> input)
        {
            RepositoryOutput<Comment> result = new();
            try
            {
                input.Input.FillKeys();
                await _context.Comments.AddAsync(input.Input);
                _ = await _context.SaveChangesAsync();
                result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentCreated), input.Input.Id);
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, CommentConstant.RepositoryCreateCommentError, new object[] { input.Input.Id });
            }

            return result;
        }

        public async Task<RepositoryOutput<Comment>> UpdateCommentAsync(RepositoryInput<Comment> input)
        {
            RepositoryOutput<Comment> result = new();
            try
            {
                Comment comment = await _context.Comments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == input.Input.Id);

                if (comment != null)
                {
                    input.Input.FillKeys();
                    _context.Comments.Update(input.Input);
                    _ = await _context.SaveChangesAsync();
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentUpdated), input.Input.Id);
                    result.Output = input.Input;
                }
                else
                {
                    result.Output = input.Input;
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentNotFound), input.Input.Id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, CommentConstant.RepositoryUpdateCommentError, new object[] { input.Input.Id });
            }

            return result;
        }

        public async Task<RepositoryOutput<bool>> RemoveCommentAsync(Guid id)
        {
            RepositoryOutput<bool> result = new();
            try
            {
                Comment comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

                if (comment != null)
                {
                    _ = _context.Comments.Remove(comment);
                    _ = await _context.SaveChangesAsync();

                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentRemoved), comment.Id);
                    result.Output = true;
                }
                else
                {
                    result.Output = false;
                    result.Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCommentNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = GenerateErrorInformation(ex, CommentConstant.RepositoryRemoveCommentError, new object[] { id });
            }

            return result;
        }
    }
}
