using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.PostValidation
{
    public class PostGuidValidation : AbstractValidator<Guid>
    {
        public PostGuidValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x)
                .Must(delegate (Guid id)
                {
                    return id != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsIdEmpty))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsIdEmpty));
        }
    }
}
