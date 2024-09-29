using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Repositories.Entities;
using Blog.Repositories.Implementations;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Blog.Validations;
using Blog.Validations.Abstractions;

namespace Blog.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly AuthorRepository _repository;
        private readonly IValidationFactory<Author> _validation;

        public AuthorService(AuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceOutput<Author>> InsertAuthorAsync(ServiceInput<Author> input)
        {
            ServiceOutput<Author> result = new();
            ValidationOutput validation = await _validation.ValidateAsync(input.Input);
            if (validation.Success)
            {
                RepositoryOutput<Author> repositoryResult = await _repository.InsertAuthorAsync(new RepositoryInput<Author>()
                {
                    Input = new Author()
                    {
                        Id = input.Input.Id,
                        Name = input.Input.Name,
                        Posts = input.Input.Posts
                    }
                });
                result.Message = repositoryResult.Message;
                result.Output = repositoryResult.Output;
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<Author>> RemoveAuthorAsync(ServiceInput<Author> input)
        {
            ServiceOutput<Author> result = new();
            ValidationOutput validation = await _validation.ValidateAsync(input.Input);
            if (validation.Success)
            {
                RepositoryOutput<Author> repositoryResult = await _repository.InsertAuthorAsync(new RepositoryInput<Author>()
                {
                    Input = new Author()
                    {
                        Id = input.Input.Id,
                        Name = input.Input.Name,
                        Posts = input.Input.Posts
                    }
                });
                result.Message = repositoryResult.Message;
                result.Output = repositoryResult.Output;
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<Author>> UpdateAuthorAsync(ServiceInput<Author> input)
        {
            throw new NotImplementedException();
        }
    }
}
