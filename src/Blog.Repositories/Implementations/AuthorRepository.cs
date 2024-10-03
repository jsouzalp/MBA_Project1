using Blog.Bases;
using Blog.Entities.Authors;
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
    public partial class AuthorRepository : IAuthorRepository
    {
        // TODO :: Colocar o tratamento de exception em um extension e com códigos de erros via translate resources

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
                Author author = await _context.Authors
                    .Include(x => x.Posts).ThenInclude(x => x.Comments).ThenInclude(x => x.CommentAuthor)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (author != null)
                {
                    result.Message = string.Format(_translateResource.GetResource(AuthorConstant.RepositoryAuthorFound), author.Name);
                    result.Output = author;
                }
                else
                {
                    result.Message = $"Autor de ID {id} não localizado";
                }
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = $"Erro localizando o autor de ID {id}",
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
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = $"Erro criando o autor {input.Input.Name}",
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
                input.Input.FillKeys();
                _context.Authors.Update(input.Input);
                _ = await _context.SaveChangesAsync();

                result.Message = $"Autor {input.Input.Name} atualizado com sucesso";
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = $"Erro atualizando o autor {input.Input.Name}",
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
                    .Include(x => x.Posts)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (author != null)
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        foreach (Post post in author.Posts)
                        {
                            _ = await _context.Comments.Where(x => x.PostId == post.Id).ExecuteDeleteAsync();
                            _ = _context.Posts.Remove(post);
                        }

                        _ = _context.Authors.Remove(author);
                        _ = await _context.SaveChangesAsync();

                        transaction.Complete();
                        result.Message = $"Autor de ID {id} removido com sucesso";
                        result.Output = true;
                    }
                }
                else
                {
                    result.Output = false;
                    result.Message = $"Autor de ID {id} não encontrado";
                }
            }
            catch (Exception ex)
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = $"Erro removendo o autor de ID {id}",
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }
    }
}
