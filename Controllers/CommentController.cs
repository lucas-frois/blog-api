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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(
            [FromRoute] long postId, 
            [FromBody] CreateCommentDto createCommentDto)
        {
            return Ok();
        }
    }
}
