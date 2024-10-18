namespace Blog.Validations.Abstractions
{
    public interface IValidationFactory<T>
    {
        Task<ValidationOutput> ValidateAsync(T input);
        Task<ValidationOutput> ValidateIdAsync(T id);
    }
}
