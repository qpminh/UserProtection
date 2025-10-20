using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Blogs;
using UserProtection.Application.Interfaces.Blogs;

namespace UserProtection.API.Controllers.Blogs
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService) => _blogService = blogService;


        [HttpGet("all")]
        public async Task<IActionResult> GetAllBlogs()
            => Ok(await _blogService.GetAllBlogsAsync());

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentBlogs([FromQuery] int count = 5)
            => Ok(await _blogService.GetRecentBlogsAsync(count));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            return blog is null
                ? NotFound(new { Message = "Blog not found" })
                : Ok(blog);
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBlogBySlug(string slug)
        {
            var blog = await _blogService.GetBlogBySlugAsync(slug);
            return blog is null
                ? NotFound(new { Message = "Blog not found" })
                : Ok(blog);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBlogs([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { Message = "Keyword cannot be empty" });

            var results = await _blogService.SearchBlogsAsync(keyword);
            return Ok(results);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _blogService.AddBlogAsync(request);
            return CreatedAtAction(nameof(GetBlogById), new { id = created.BlogId }, created);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] UpdateBlogRequest request)
        {
            var updated = await _blogService.UpdateBlogAsync(id, request);
            return updated is null
                ? NotFound(new { Message = "Blog not found" })
                : Ok(updated);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var deleted = await _blogService.DeleteBlogAsync(id);
            return !deleted
                ? NotFound(new { Message = "Blog not found" })
                : Ok(new { Message = "Blog deleted successfully" });
        }
    }
}
