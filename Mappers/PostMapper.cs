﻿using Blog.API.Dtos;
using Blog.API.Models;

namespace Blog.API.Mappers
{
    public static class PostMapper
    {
        public static Post ToEntity(this PostDto postDto)
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
                Title = post.Title,
                Content = post.Content,
            };

            return postDto;
        }
    }
}
