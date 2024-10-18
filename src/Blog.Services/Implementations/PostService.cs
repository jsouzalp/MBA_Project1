using AutoMapper;
using Blog.Bases.Services;
using Blog.Entities.Posts;
using Blog.Repositories.Abstractions;
using Blog.Repositories.Entities;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Blog.Validations;
using Blog.Validations.Abstractions;
using System.Collections.Generic;

namespace Blog.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IValidationFactory<PostInput> _postValidation;
        private readonly IValidationFactory<FilterPostInput> _filterPostValidation;
        private readonly IValidationFactory<Guid> _idValidation;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repository,
            IValidationFactory<PostInput> postValidation,
            IValidationFactory<FilterPostInput> filterPostValidation,
            IValidationFactory<Guid> idValidation,
            IMapper mapper)
        {
            _repository = repository;
            _postValidation = postValidation;
            _filterPostValidation = filterPostValidation;
            _idValidation = idValidation;
            _mapper = mapper;
        }

        public async Task<ServiceOutput<IEnumerable<PostOutput>>> FilterPostsAsync(FilterPostInput input)
        {
            ServiceOutput<IEnumerable<PostOutput>> result = new();
            ValidationOutput validation = await _filterPostValidation.ValidateIdAsync(input);

            if (validation.Success)
            {
                RepositoryOutput<IEnumerable<Post>> repositoryResult = await _repository.FilterPostsAsync(new RepositoryInput<FilterPostInput>()
                {
                    Input = input
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<IEnumerable<PostOutput>>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<PostOutput>> CreatePostAsync(ServiceInput<PostInput> input)
        {
            ServiceOutput<PostOutput> result = new();
            ValidationOutput validation = await _postValidation.ValidateAsync(input.Input);

            if (validation.Success)
            {
                RepositoryOutput<Post> repositoryResult = await _repository.CreatePostAsync(new RepositoryInput<Post>()
                {
                    Input = _mapper.Map<Post>(input.Input)
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<PostOutput>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<PostOutput>> UpdatePostAsync(ServiceInput<PostInput> input)
        {
            ServiceOutput<PostOutput> result = new();
            ValidationOutput validation = await _postValidation.ValidateAsync(input.Input);
            if (validation.Success)
            {
                RepositoryOutput<Post> repositoryResult = await _repository.UpdatePostAsync(new RepositoryInput<Post>()
                {
                    Input = _mapper.Map<Post>(input.Input)
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<PostOutput>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<bool>> RemovePostAsync(Guid id)
        {
            ServiceOutput<bool> result = new();
            ValidationOutput validation = await _idValidation.ValidateIdAsync(id);

            if (validation.Success)
            {
                RepositoryOutput<bool> repositoryResult = await _repository.RemovePostAsync(id);
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
    }
}
