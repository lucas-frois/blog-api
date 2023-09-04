namespace Blog.API.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] Salt { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        /// <summary>
        /// EF relationship fields
        /// </summary>
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
