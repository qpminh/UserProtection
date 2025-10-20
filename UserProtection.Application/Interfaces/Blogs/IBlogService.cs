using UserProtection.Application.Dtos.Blogs;

namespace UserProtection.Application.Interfaces.Blogs
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDto>> GetAllBlogsAsync();
        Task<IEnumerable<BlogDto>> GetRecentBlogsAsync(int count = 5);
        Task<BlogDto?> GetBlogByIdAsync(int blogId);
        Task<BlogDto?> GetBlogBySlugAsync(string slug);
        Task<IEnumerable<BlogDto>> SearchBlogsAsync(string keyword);
        Task<BlogDto> AddBlogAsync(CreateBlogRequest request);
        Task<BlogDto?> UpdateBlogAsync(int blogId, UpdateBlogRequest request);
        Task<bool> DeleteBlogAsync(int blogId);
    }
}
