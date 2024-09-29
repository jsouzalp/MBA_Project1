namespace Blog.Validations.Abstractions
{
    public interface IValidationFactory<T> where T : class
    {
        Task<ValidationOutput> ValidateAsync(T input);
    }
}
