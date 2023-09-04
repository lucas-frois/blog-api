using Blog.API.Dtos;
using Blog.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1, 
            [FromQuery] int size = 20)
        {
            var posts = await postService.GetAll(page, size);

            return Ok(posts);
        }

        [HttpGet("~/api/writers/posts")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> GetFromWriter(
            [FromQuery] int page = 1,
            [FromQuery] int size = 20)
        {
            //var user = User.Identity.Name;

            var posts = await postService.GetAll(page, size);

            return Ok(posts);
        }


        [HttpGet("pending")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> SearchPending(
            [FromQuery] long page = 1, 
            [FromQuery] long size = 20)
        {

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> Create([FromBody] PostDto postDto)
        {
            await postService.Create(postDto);

            return NoContent();
        }

        [HttpPut("{postId:long}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> Update([FromRoute] long postId, [FromBody] PostDto postDto)
        {
            await postService.Update(postId, postDto);

            return Ok();
        }

        [HttpPatch("{postId:long}/submit")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> Submit([FromRoute] long postId)
        {
            return Ok();
        }

        [HttpPatch("{postId:long}/approve")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> ApprovePostReview([FromRoute] long postId)
        {
            await postService.Review(postId, approved: true);

            return Ok();
        }

        [HttpPatch("{postId:long}/reject")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> RejectPostReview([FromRoute] long postId, [FromBody] PostDenyDto postDenyDto)
        {
            await postService.Review(postId, approved: false);

            return Ok();
        }
    }
}
