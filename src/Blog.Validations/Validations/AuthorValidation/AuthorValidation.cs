using Blog.Entities.Authors;
using Blog.Entities.Posts;
using FluentValidation;

namespace Blog.Validations.Validations.AuthorValidation
{
    public class AuthorValidation : AbstractValidator<AuthorInput>
    {
        public AuthorValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode("2000")
                .WithMessage("Nome não pode ser vazio")
                .NotNull()
                .WithErrorCode("2010")
                .WithMessage("Nome não pode ser nulo");


            RuleFor(x => x.Posts)
                .Must(delegate (ICollection<PostInput> posts)
                {
                    return !posts?.Any(x => string.IsNullOrWhiteSpace(x.Title)) ?? true;
                })
                .WithErrorCode("2020")
                .WithMessage("Postagens possui título não preenchido");

            RuleFor(x => x.Posts)
                .Must(delegate (ICollection<PostInput> posts)
                {
                    return !posts?.Any(x => string.IsNullOrWhiteSpace(x.Message)) ?? true;
                })
                .WithErrorCode("2030")
                .WithMessage("Postagens possui mensagem não preenchida");
        }
    }
}
