namespace Blog.API.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Salt { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public enum UserRole
    {
        Viewer = 1, 
        Writer = 2, 
        Editor = 3
    }
}
