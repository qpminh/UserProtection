using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Blogs
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();                // Lấy tất cả blog
        Task<IEnumerable<Blog>> GetRecentBlogsAsync(int count = 5); // Lấy n blog mới nhất
        Task<Blog?> GetBlogByIdAsync(int blogId);                  // Lấy blog theo ID
        Task<Blog?> GetBlogBySlugAsync(string slug);               // Lấy blog theo Slug
        Task<IEnumerable<Blog>> SearchBlogsAsync(string keyword);  // Tìm blog theo từ khóa
        Task AddBlogAsync(Blog blog);                              // Thêm blog mới
        Task UpdateBlogAsync(Blog blog);                           // Cập nhật blog
        Task DeleteBlogAsync(int blogId);                          // Xóa blog
    }
}
