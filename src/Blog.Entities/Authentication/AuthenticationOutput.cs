namespace Blog.Entities.Authentication
{
    public class AuthenticationOutput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string AccessToken { get; set; }
    }
}
