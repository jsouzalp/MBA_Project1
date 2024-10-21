using Blog.Entities.Authentication;
using Blog.Entities.Posts;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class AuthenticationValidation : AbstractValidator<AuthenticationInput>
    {
        public AuthenticationValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(AuthenticationConstant.ServiceValidationEmptyFullName))
                .WithMessage(translateResource.GetResource(AuthenticationConstant.ServiceValidationEmptyFullName))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(AuthenticationConstant.ServiceValidationNullFullName))
                .WithMessage(translateResource.GetResource(AuthenticationConstant.ServiceValidationNullFullName));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(AuthenticationConstant.ServiceValidationEmptyEmail))
                .WithMessage(translateResource.GetResource(AuthenticationConstant.ServiceValidationEmptyEmail))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(AuthenticationConstant.ServiceValidationNullEmail))
                .WithMessage(translateResource.GetResource(AuthenticationConstant.ServiceValidationNullEmail));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(AuthenticationConstant.ServiceValidationEmptyPassword))
                .WithMessage(translateResource.GetResource(AuthenticationConstant.ServiceValidationEmptyPassword))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(AuthenticationConstant.ServiceValidationNullPassword))
                .WithMessage(translateResource.GetResource(AuthenticationConstant.ServiceValidationNullPassword));
        }
    }
}
