using System;

namespace Blog.API.Models
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Status = PostStatusEnum.Created;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public PostStatusEnum Status { get; set; }

        /// <summary>
        /// EF relationship fields
        /// </summary>
        public long AuthorId { get; set; }
        public User Author { get; set; }
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
