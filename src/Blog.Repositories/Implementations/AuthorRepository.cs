using Blog.Bases;
using Blog.Entities.Authors;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.Repositories.Implementations
{
    public partial class AuthorRepository : IAuthorRepository
    {
        // TODO :: Colocar o tratamento de exception em um extension

        private readonly BlogDbContext _context;
        private readonly ITranslationResource _translateResource;

        public AuthorRepository(BlogDbContext context, ITranslationResource translateResource)
        {
            _context = context;
            _translateResource = translateResource;
        }

        public async Task<RepositoryOutput<Author>> GetAuthorByIdAsync(Guid id)
        {
            RepositoryOutput<Author> result = new();
            try
            {
                result.Output = await _context.Authors
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (result.Output != null)
                {
                    result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorFound), result.Output.Name);
                }
                else
                {
                    result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(AuthorConstant.RepositoryGetAuthorError),
                        Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryGetAuthorError), id),
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }

        public async Task<RepositoryOutput<Author>> CreateAuthorAsync(RepositoryInput<Author> input)
        {
            RepositoryOutput<Author> result = new();
            try
            {
                input.Input.FillKeys();
                await _context.Authors.AddAsync(input.Input);
                _ = await _context.SaveChangesAsync();
                result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorCreated), input.Input.Name);
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(AuthorConstant.RepositoryInsertAuthorError),
                        Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryInsertAuthorError), input.Input.Name),
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }

        public async Task<RepositoryOutput<Author>> UpdateAuthorAsync(RepositoryInput<Author> input)
        {
            RepositoryOutput<Author> result = new();
            try
            {
                Author author = await _context.Authors
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == input.Input.Id);

                if (author != null)
                {
                    input.Input.FillKeys();
                    input.Input.IdentityUser = author.IdentityUser;
                    _context.Authors.Update(input.Input);
                    _ = await _context.SaveChangesAsync();
                    result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorUpdated), input.Input.Name);
                    result.Output = input.Input;
                }
                else
                {
                    result.Output = input.Input;
                    result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorNotFound), input.Input.Id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(AuthorConstant.RepositoryUpdateAuthorError),
                        Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryUpdateAuthorError), input.Input.Name),
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }

        public async Task<RepositoryOutput<bool>> RemoveAuthorAsync(Guid id)
        {
            RepositoryOutput<bool> result = new();
            try
            {
                Author author = await _context.Authors
                    .Include(x => x.Posts).ThenInclude(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (author != null)
                {
                    //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        foreach (Post post in author.Posts)
                        {
                            foreach (Comment comment in post.Comments)
                            {
                                _context.Comments.Remove(comment);
                            }
                            _ = _context.Posts.Remove(post);
                        }
                        _ = _context.Authors.Remove(author);
                        _ = await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        //transaction.Complete();
                        result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorRemoved), author.Name);
                        result.Output = true;
                    }
                }
                else
                {
                    result.Output = false;
                    result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorNotFound), id);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(AuthorConstant.RepositoryRemoveAuthorError),
                        Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryRemoveAuthorError), id),
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }
    }
}
