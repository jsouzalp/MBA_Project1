using AutoMapper;
using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Repositories.Abstractions;
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
        private readonly IAuthorRepository _repository;
        private readonly IValidationFactory<AuthorInput> _authorValidation;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, 
            IValidationFactory<AuthorInput> authorValidation, 
            IMapper mapper)
        {
            _repository = repository;
            _authorValidation = authorValidation;
            _mapper = mapper;
        }

        public async Task<ServiceOutput<AuthorOutput>> GetAuthorByIdAsync(Guid id)
        {
            RepositoryOutput<Author> repositoryResult = await _repository.GetAuthorByIdAsync(id);
            return new ServiceOutput<AuthorOutput>()
            {
                Message = repositoryResult.Message,
                Output = _mapper.Map<AuthorOutput>(repositoryResult.Output),
                Errors = repositoryResult.Errors
            };
        }

        public async Task<ServiceOutput<AuthorOutput>> CreateAuthorAsync(ServiceInput<AuthorInput> input)
        {
            ServiceOutput<AuthorOutput> result = new();
            ValidationOutput validation = await _authorValidation.ValidateAsync(input.Input);

            if (validation.Success)
            {
                RepositoryOutput<Author> repositoryResult = await _repository.CreateAuthorAsync(new RepositoryInput<Author>()
                {
                    Input = _mapper.Map<Author>(input.Input)
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<AuthorOutput>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<AuthorOutput>> UpdateAuthorAsync(ServiceInput<AuthorInput> input)
        {
            ServiceOutput<AuthorOutput> result = new();
            ValidationOutput validation = await _authorValidation.ValidateAsync(input.Input);
            if (validation.Success)
            {
                RepositoryOutput<Author> repositoryResult = await _repository.UpdateAuthorAsync(new RepositoryInput<Author>()
                {
                    Input = _mapper.Map<Author>(input.Input)
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<AuthorOutput>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<bool>> RemoveAuthorAsync(Guid id)
        {
            ServiceOutput<bool> result = new();

            RepositoryOutput<bool> repositoryResult = await _repository.RemoveAuthorAsync(id);
            result.Message = repositoryResult.Message;
            result.Output = repositoryResult.Output;
            result.Errors = repositoryResult.Errors;

            return result;
        }
    }
}
