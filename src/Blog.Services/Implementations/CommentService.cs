using AutoMapper;
using Blog.Bases.Services;
using Blog.Entities.Comments;
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
    public class CommentService : ServiceBase, ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IValidationFactory<CommentInput> _commentValidation;
        private readonly IValidationFactory<Guid> _idValidation;
        private readonly IMapper _mapper;

        public CommentService(IHttpContextAccessor httpContextAccessor,
            ITranslationResource translationResource,
            ICommentRepository repository,
            IValidationFactory<CommentInput> postValidation,
            IValidationFactory<Guid> idValidation,
            IMapper mapper) : base(httpContextAccessor, translationResource)
        {
            _repository = repository;
            _commentValidation = postValidation;
            _idValidation = idValidation;
            _mapper = mapper;
        }

        public async Task<ServiceOutput<CommentOutput>> GetCommentAsync(Guid id)
        {
            ServiceOutput<CommentOutput> result = new();
            ValidationOutput validation = await _idValidation.ValidateIdAsync(id);

            if (validation.Success)
            {
                RepositoryOutput<Comment> internalComment = await _repository.GetInternalCommentAsync(id);
                if (ValidateOwnerOrAdmin(internalComment.Output.CommentAuthorId, result))
                {
                    RepositoryOutput<Comment> repositoryResult = await _repository.GetCommentAsync(id);
                    result.Message = repositoryResult.Message;
                    result.Output = _mapper.Map<CommentOutput>(repositoryResult.Output);
                    result.Errors = repositoryResult.Errors;
                }
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<CommentOutput>> CreateCommentAsync(ServiceInput<CommentInput> input)
        {
            ServiceOutput<CommentOutput> result = new();
            ValidationOutput validation = await _commentValidation.ValidateAsync(input.Input);

            if (validation.Success && ValidateOwnerOrAdmin(input.Input.CommentAuthorId, result))
            {
                RepositoryOutput<Comment> repositoryResult = await _repository.CreateCommentAsync(new RepositoryInput<Comment>()
                {
                    Input = _mapper.Map<Comment>(input.Input)
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<CommentOutput>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                if (validation.Errors != null) { result.Errors = validation.Errors; }
            }

            return result;
        }

        public async Task<ServiceOutput<CommentOutput>> UpdateCommentAsync(ServiceInput<CommentInput> input)
        {
            ServiceOutput<CommentOutput> result = new();
            ValidationOutput validation = await _commentValidation.ValidateAsync(input.Input);
            if (validation.Success && ValidateOwnerOrAdmin(input.Input.CommentAuthorId, result))
            {
                RepositoryOutput<Comment> repositoryResult = await _repository.UpdateCommentAsync(new RepositoryInput<Comment>()
                {
                    Input = _mapper.Map<Comment>(input.Input)
                });
                result.Message = repositoryResult.Message;
                result.Output = _mapper.Map<CommentOutput>(repositoryResult.Output);
                result.Errors = repositoryResult.Errors;
            }
            else
            {
                if (validation.Errors != null) { result.Errors = validation.Errors; }
            }

            return result;
        }

        public async Task<ServiceOutput<bool>> RemoveCommentAsync(Guid id)
        {
            ServiceOutput<bool> result = new();
            ValidationOutput validation = await _idValidation.ValidateIdAsync(id);

            if (validation.Success)
            {
                RepositoryOutput<Comment> internalComment = await _repository.GetInternalCommentAsync(id);
                if (ValidateOwnerOrAdmin(internalComment.Output.CommentAuthorId, result))
                {
                    RepositoryOutput<bool> repositoryResult = await _repository.RemoveCommentAsync(id);
                    result.Message = repositoryResult.Message;
                    result.Output = repositoryResult.Output;
                    result.Errors = repositoryResult.Errors;
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
