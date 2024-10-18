using Blog.Entities.Posts;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class FilterPostValidation : AbstractValidator<FilterPostInput>
    {
        public FilterPostValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x.AuthorId)
                .Must(delegate (Guid authorId)
                {
                    return authorId != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsAuthorIdEmpty))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsAuthorIdEmpty));

            RuleFor(x => x.Page)
                .NotEqual(0)
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsPostFilterPage))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsPostFilterPage));

            RuleFor(x => x.RecordsPerPage)
                .NotEqual(0)
                .WithErrorCode(translateResource.GetCodeResource(PostConstant.ValidationsPostFilterRecordsPerPage))
                .WithMessage(translateResource.GetResource(PostConstant.ValidationsPostFilterRecordsPerPage));
        }
    }
}
