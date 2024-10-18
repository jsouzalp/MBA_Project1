using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class CommentGuidValidation : AbstractValidator<Guid>
    {
        public CommentGuidValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x)
                .Must(delegate (Guid id)
                {
                    return id != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(CommentConstant.ValidationsIdEmpty))
                .WithMessage(translateResource.GetResource(CommentConstant.ValidationsIdEmpty));
        }
    }
}
