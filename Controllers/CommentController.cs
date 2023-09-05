using Blog.API.Dtos;
using Blog.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/comments")]
    public class CommentController : Controller
    {
        private readonly IPostService postService;

        public CommentController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(
            [FromRoute] long postId, 
            [FromBody] CreateCommentDto createCommentDto)
        {
            long userId = 0;

            await postService.AddComment(userId, postId, createCommentDto.Content);

            return NoContent();
        }
    }
}
