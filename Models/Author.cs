namespace Blog.API.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Salt { get; set; }
        public AuthorRole Role { get; set; }
    }

    public enum AuthorRole
    {
        Public = 1, 
        Writer = 2, 
        Editor = 3
    }
}
