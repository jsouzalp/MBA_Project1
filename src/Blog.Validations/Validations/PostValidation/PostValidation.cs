using Blog.Entities.Posts;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class PostValidation : AbstractValidator<PostInput>
    {
        public PostValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x.AuthorId)
                .Must(delegate (Guid authorId)
                {
                    return authorId != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsAuthorIdEmpty))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsAuthorIdEmpty));

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsPostEmptyTitle))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsPostEmptyTitle))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsPostNullTitle))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsPostNullTitle));


            RuleFor(x => x.Message)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsPostEmptyMessage))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsPostEmptyMessage))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsPostNullMessage))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsPostNullMessage));            
        }
    }
}
