namespace Blog.API.Dtos
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public UserDto Author { get; set; }
        public IList<CommentDto> Comments { get; set; }
    }

    public class CommentDto
    {
        public UserDto Author { get; set; }
        public string Content { get; set; }
    }

    public class UserDto
    {
        public string Name { get; set; }
    }
}
