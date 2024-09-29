using Blog.Entities.Authors;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class AuthorInsertValidation : AbstractValidator<Author>
    {
        public AuthorInsertValidation()
        {
            //RuleFor(x => x.ObjectId)
            //    .NotEqual(Guid.Empty)
            //    .WithErrorCode(translation.GetCodeResource(MessagesConstant.UpdateEntityDetailValidationInvalidObjectId))
            //    .WithMessage(translation.GetResource(MessagesConstant.UpdateEntityDetailValidationInvalidObjectId));
           

            //RuleFor(x => x.Nif)
            //    .Must(delegate (string nif)
            //    {
            //        return validateNif.ValidateNifSize(nif, true);
            //    })
            //    .WithErrorCode(translation.GetCodeResource(MessagesConstant.UpdateEntityDetailValidationInvalidNifSize))
            //    .WithMessage(translation.GetResource(MessagesConstant.UpdateEntityDetailValidationInvalidNifSize));
        }
    }
}
