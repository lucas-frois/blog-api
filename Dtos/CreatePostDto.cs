using Blog.API.Models;

namespace Blog.API.Dtos
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
