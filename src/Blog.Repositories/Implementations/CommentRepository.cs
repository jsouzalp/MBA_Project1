using Blog.Bases;
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
    public partial class CommentRepository : ICommentRepository
    {
        // TODO :: Colocar o tratamento de exception em um extension

        private readonly BlogDbContext _context;
        private readonly ITranslationResource _translateResource;

        public CommentRepository(BlogDbContext context, ITranslationResource translateResource)
        {
            _context = context;
            _translateResource = translateResource;
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(CommentConstant.RepositorySelectCommentError),
                        Message = string.Format(_translateResource.GetResource(CommentConstant.RepositorySelectCommentError), id),
                        InternalMessage = ex.ToString()
                    }
                };
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(CommentConstant.RepositoryCreateCommentError),
                        Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryCreateCommentError), input.Input.Id),
                        InternalMessage = ex.ToString()
                    }
                };
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(CommentConstant.RepositoryUpdateCommentError),
                        Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryUpdateCommentError), input.Input.Id),
                        InternalMessage = ex.ToString()
                    }
                };
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
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(CommentConstant.RepositoryRemoveCommentError),
                        Message = string.Format(_translateResource.GetResource(CommentConstant.RepositoryRemoveCommentError), id),
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }
    }
}
