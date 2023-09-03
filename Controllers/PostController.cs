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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDto postDto)
        {
            await postService.Create(postDto);

            return NoContent();
        }
    }
}
