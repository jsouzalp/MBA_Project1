using Blog.Bases;
using Blog.Entities.Authors;
using Blog.Repositories.Abstractions;
using Blog.Repositories.Contexts;
using Blog.Repositories.Entities;
using Blog.Repositories.Extensions;

namespace Blog.Repositories.Implementations
{
    public partial class AuthorRepository : IAuthorRepository
    {
        private readonly BlogDbContext _context;
        public AuthorRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<RepositoryOutput<Author>> InsertAuthorAsync(RepositoryInput<Author> input)
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
                // TODO :: Colocar o tratamento de exception em um extension e com códigos de erros via translate resources
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = "Erro criando um Autor",
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
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                // TODO :: Colocar o tratamento de exception em um extension e com códigos de erros via translate resources
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = "Erro atualizando um Autor",
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }

        public async Task<RepositoryOutput<Author>> RemoveAuthorAsync(RepositoryInput<Author> input)
        {
            RepositoryOutput<Author> result = new();
            try
            {
                input.Input.FillKeys();
                _context.Authors.Remove(input.Input);
                _ = await _context.SaveChangesAsync();
                result.Output = input.Input;
            }
            catch (Exception ex)
            {
                // TODO :: Colocar o tratamento de exception em um extension e com códigos de erros via translate resources
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = "1000",
                        Message = "Erro removendo um Autor",
                        InternalMessage = ex.ToString()
                    }
                };
            }

            return result;
        }
    }
}
