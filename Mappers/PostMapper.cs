using Blog.API.Dtos;
using Blog.API.Models;

namespace Blog.API.Mappers
{
    public static class PostMapper
    {
        public static Post ToEntity(this CreatePostDto postDto)
        {
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
            };

            return post;
        }

        public static PostDto ToDto(this Post post)
        {
            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Status = Enum.GetName(typeof(PostStatusEnum), post.Status) ?? "unknown"
            };

            return postDto;
        }
    }
}
