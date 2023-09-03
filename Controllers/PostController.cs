using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {

            return NoContent();
        }
    }
}
