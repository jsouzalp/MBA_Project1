namespace Blog.Entities.Posts
{
    public class FilterPostInput
    {
        public Guid? AuthorId { get; set; }
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public int Skip { get => (Page * RecordsPerPage) - RecordsPerPage; }
    }
}
