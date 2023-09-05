using Microsoft.Extensions.Hosting;

namespace Blog.API.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] Salt { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public UserRoleEnum UserRoleEnum => (UserRoleEnum) Enum.Parse(typeof(UserRoleEnum), Role);

        /// <summary>
        /// EF relationship fields
        /// </summary>
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public enum UserRoleEnum
    {
        VIEWER, 
        WRITER, 
        EDITOR
    }
}
