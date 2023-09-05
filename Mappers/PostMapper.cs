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
                Status = Enum.GetName(typeof(PostStatusEnum), post.PostStatusEnum) ?? "unknown", 
                Author = new UserDto
                {
                    Name = post.User.Name,
                },
                Comments = post.Comments.Select(c => new CommentDto 
                {
                    Content = c.Content, 
                    Author = new UserDto
                    {
                        Name = c.User.Name,
                    }
                }).ToList(),
            };

            return postDto;
        }
    }
}
