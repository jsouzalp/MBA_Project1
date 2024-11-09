﻿using AutoMapper;
using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Repositories.Abstractions;
using Blog.Repositories.Entities;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Blog.Translations.Abstractions;
using Blog.Validations;
using Blog.Validations.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Blog.Services.Implementations
{
    public class AuthorService : ServiceBase, IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IValidationFactory<AuthorInput> _authorValidation;
        private readonly IValidationFactory<Guid> _idValidation;
        private readonly IMapper _mapper;

        public AuthorService(IHttpContextAccessor httpContextAccessor,
            ITranslationResource translationResource,
            IAuthorRepository repository,
            IValidationFactory<AuthorInput> authorValidation,
            IValidationFactory<Guid> idValidation,
            IMapper mapper) : base(httpContextAccessor, translationResource)
        {
            _repository = repository;
            _authorValidation = authorValidation;
            _idValidation = idValidation;
            _mapper = mapper;
        }

        public async Task<ServiceOutput<AuthorOutput>> GetAuthorByIdAsync(Guid id)
        {
            ServiceOutput<AuthorOutput> result = new();
            ValidationOutput validation = await _idValidation.ValidateIdAsync(id);

            if (validation.Success)
            {
                if (ValidateOwnerOrAdmin(id, result))
                {
                    RepositoryOutput<Author> repositoryResult = await _repository.GetAuthorByIdAsync(id);
                    result.Message = repositoryResult.Message;
                    result.Output = _mapper.Map<AuthorOutput>(repositoryResult.Output);
                    result.Errors = repositoryResult.Errors;
                }
            }
            else
            {
                if (validation.Errors != null) { result.Errors = validation.Errors; }
            }

            return result;
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
                if (validation.Success && ValidateOwnerOrAdmin(input.Input.Id, result))
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
                    if (validation.Errors != null) { result.Errors = validation.Errors; }
                }
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
            ValidationOutput validation = await _idValidation.ValidateIdAsync(id);

            if (validation.Success)
            {
                if (validation.Success && ValidateOwnerOrAdmin(id, result))
                {
                    RepositoryOutput<bool> repositoryResult = await _repository.RemoveAuthorAsync(id);
                    result.Message = repositoryResult.Message;
                    result.Output = repositoryResult.Output;
                    result.Errors = repositoryResult.Errors;
                }
                else
                {
                    if (validation.Errors != null) { result.Errors = validation.Errors; }
                }
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }
    }
}
