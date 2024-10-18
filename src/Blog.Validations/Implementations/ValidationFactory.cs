using Blog.Bases;
using Blog.Validations.Abstractions;
using FluentValidation;

namespace Blog.Validations.Implementations
{
    public class ValidationFactory<T> : IValidationFactory<T> 
    {
        private readonly IValidator<T> _validator;
        public ValidationFactory(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task<ValidationOutput> ValidateAsync(T input)
        {
            var resultValidation = await _validator.ValidateAsync(input);
            ValidationOutput output = new ValidationOutput();
            if (!resultValidation.IsValid)
            {
                output.Errors = (from x in resultValidation.Errors
                                 select new ErrorBase()
                                 {
                                     Code = x.ErrorCode,
                                     Message = x.ErrorMessage,
                                     InternalMessage = x.PropertyName
                                 }).ToList();
            }

            return output;
        }

        public async Task<ValidationOutput> ValidateIdAsync(T id)
        {
            var resultValidation = await _validator.ValidateAsync(id);
            ValidationOutput output = new ValidationOutput();
            if (!resultValidation.IsValid)
            {
                output.Errors = (from x in resultValidation.Errors
                                 select new ErrorBase()
                                 {
                                     Code = x.ErrorCode,
                                     Message = x.ErrorMessage,
                                     InternalMessage = x.PropertyName
                                 }).ToList();
            }

            return output;
        }
    }
}
