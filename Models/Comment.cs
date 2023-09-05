namespace Blog.API.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public bool IsVisibleOnlyToWriter => User.RoleEnum == UserRole.Editor;

        /// <summary>
        /// EF relationship fields
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
    }
}
