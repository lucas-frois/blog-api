namespace Blog.API.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public User Author { get; set; }
        public bool IsVisibleOnlyToWriter => Author.RoleEnum == UserRole.Editor;
    }
}
