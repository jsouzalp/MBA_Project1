using Blog.Bases;

namespace Blog.Validations
{
    public class ValidationOutput 
    {
        public bool Success { get => Errors == null || !Errors.Any(); }

        public IList<ErrorBase> Errors { get; set; }
    }
}
