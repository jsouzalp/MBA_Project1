using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations
{
    public class GuidValidation : AbstractValidator<Guid>
    {
        public GuidValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x)
                .Must(delegate (Guid id)
                {
                    return id != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(GuidConstant.ValidationsIdEmpty))
                .WithMessage(translateResource.GetResource(GuidConstant.ValidationsIdEmpty));
        }
    }
}
