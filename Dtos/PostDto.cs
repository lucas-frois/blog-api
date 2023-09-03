using Blog.API.Models;

namespace Blog.API.Dtos
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}
