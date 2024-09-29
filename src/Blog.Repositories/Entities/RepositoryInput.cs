namespace Blog.Repositories.Entities
{
    public class RepositoryInput<T> where T:class
    {
        public T Input { get; set; }
    }
}
