using Blog.API.Dtos;
using Blog.API.Services;
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
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1, 
            [FromQuery] int size = 20)
        {
            var posts = await postService.GetAll(page, size);

            return Ok(posts);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string status, 
            [FromQuery] long page = 1, 
            [FromQuery] long size = 20)
        {

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDto postDto)
        {
            await postService.Create(postDto);

            return NoContent();
        }

        [HttpPut("{postId:long}")]
        public async Task<IActionResult> Update([FromRoute] long postId, [FromBody] PostDto postDto)
        {
            await postService.Update(postId, postDto);

            return Ok();
        }


        [HttpPatch("{postId:long}/approve")]
        public async Task<IActionResult> ApprovePostReview([FromRoute] long postId)
        {

            await postService.Review(postId, approved: true);

            return Ok();
        }

        [HttpPatch("{postId:long}/reject")]
        public async Task<IActionResult> RejectPostReview([FromRoute] long postId)
        {
            await postService.Review(postId, approved: false);

            return Ok();
        }
    }
}
