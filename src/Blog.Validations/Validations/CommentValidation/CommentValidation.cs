using Blog.Entities.Comments;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class CommentValidation : AbstractValidator<CommentInput>
    {
        public CommentValidation(ITranslationResource translateResource)
        {
            RuleFor(x => x.PostId)
                .Must(delegate (Guid postId)
                {
                    return postId != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(CommentConstant.ValidationsPostIdEmpty))
                .WithMessage(translateResource.GetResource(CommentConstant.ValidationsPostIdEmpty));

            RuleFor(x => x.CommentAuthorId)
                .Must(delegate (Guid commentAuthorId)
                {
                    return commentAuthorId != Guid.Empty;
                })
                .WithErrorCode(translateResource.GetCodeResource(CommentConstant.ValidationsCommentAuthorIdEmpty))
                .WithMessage(translateResource.GetResource(CommentConstant.ValidationsCommentAuthorIdEmpty));

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithErrorCode(translateResource.GetCodeResource(CommentConstant.ValidationsCommentEmptyMessage))
                .WithMessage(translateResource.GetResource(CommentConstant.ValidationsCommentEmptyMessage))
                .NotNull()
                .WithErrorCode(translateResource.GetCodeResource(CommentConstant.ValidationsCommentNullMessage))
                .WithMessage(translateResource.GetResource(CommentConstant.ValidationsCommentNullMessage));
        }
    }
}
