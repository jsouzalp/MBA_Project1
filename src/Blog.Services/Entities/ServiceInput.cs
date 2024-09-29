namespace Blog.Bases.Services
{
    public class ServiceInput<T> where T:class
    {
        public T Input { get; set; }
    }
}
