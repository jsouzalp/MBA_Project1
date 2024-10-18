using Blog.Entities.Authors;
using Blog.Entities.Posts;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class AuthorValidation : AbstractValidator<AuthorInput>
    {
        public AuthorValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(AuthorConstant.ValidationsAuthorEmptyName))
                .WithMessage(translateResource.GetResource(AuthorConstant.ValidationsAuthorEmptyName))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(AuthorConstant.ValidationsAuthorNullName))
                .WithMessage(translateResource.GetResource(AuthorConstant.ValidationsAuthorNullName));

            RuleFor(x => x.Posts)
                .Must(delegate (ICollection<PostInput> posts)
                {
                    return !posts?.Any(x => string.IsNullOrWhiteSpace(x.Title)) ?? true;
                })
                .WithErrorCode(translateResource.GetCodeResource(AuthorConstant.ValidationsAuthorPostWithoutTitle))
                .WithMessage(translateResource.GetResource(AuthorConstant.ValidationsAuthorPostWithoutTitle));

            RuleFor(x => x.Posts)
                .Must(delegate (ICollection<PostInput> posts)
                {
                    return !posts?.Any(x => string.IsNullOrWhiteSpace(x.Message)) ?? true;
                })
                .WithErrorCode(translateResource.GetCodeResource(AuthorConstant.ValidationsAuthorPostWithoutMessage))
                .WithMessage(translateResource.GetResource(AuthorConstant.ValidationsAuthorPostWithoutMessage));
        }
    }
}
