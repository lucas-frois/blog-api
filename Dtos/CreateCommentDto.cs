namespace Blog.API.Dtos
{
    public class CreateCommentDto
    {
        public long UserId { get; set; }
        public string Content { get; set; }
    }
}
