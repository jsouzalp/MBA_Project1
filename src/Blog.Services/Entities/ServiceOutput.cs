using Blog.Bases;

namespace Blog.Services.Entities
{
    public class ServiceOutput<T> //where T : class
    {
        public bool Success { get => Errors == null || !Errors.Any(); }
        public string Message { get; set; }
        public T Output { get; set; }
        public IEnumerable<ErrorBase> Errors { get; set; }
    }
}
