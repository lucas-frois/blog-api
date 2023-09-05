using System;

namespace Blog.API.Models
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Status = "CREATED";
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public PostStatusEnum StatusEnum => (PostStatusEnum) Enum.Parse(typeof(PostStatusEnum), Status);

        /// <summary>
        /// EF relationship fields
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public enum PostStatusEnum
    {
        Created = 1, 
        Submitted = 2, 
        Published = 3, 
        Rejected = 4
    }
}
