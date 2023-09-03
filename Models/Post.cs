using System;

namespace Blog.API.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public PostStatus Status { get; set; } = PostStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum PostStatus
    {
        Pending = 1, 
        Approved = 2, 
        Rejected = 3
    }
}
