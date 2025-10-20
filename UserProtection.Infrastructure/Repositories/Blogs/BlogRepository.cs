using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Blogs;

namespace UserProtection.Infrastructure.Repositories.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly UserProtectionContext _context;
        public BlogRepository(UserProtectionContext context) => _context = context;

        // =============== GET ALL BLOGS ===============
        public async Task<IEnumerable<Blog>> GetAllBlogsAsync() =>
            await _context.Blogs
                .OrderByDescending(b => b.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

        // =============== GET RECENT BLOGS ===============
        public async Task<IEnumerable<Blog>> GetRecentBlogsAsync(int count = 5) =>
            await _context.Blogs
                .OrderByDescending(b => b.CreatedAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

        // =============== GET BY ID ===============
        public async Task<Blog?> GetBlogByIdAsync(int blogId) =>
            await _context.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BlogId == blogId);

        // =============== GET BY SLUG ===============
        public async Task<Blog?> GetBlogBySlugAsync(string slug) =>
            await _context.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Slug == slug);

        // =============== SEARCH BLOGS ===============
        public async Task<IEnumerable<Blog>> SearchBlogsAsync(string keyword)
        {
            keyword = keyword.ToLower().Trim();
            return await _context.Blogs
                .Where(b =>
                    b.Title.ToLower().Contains(keyword) ||
                    (b.Summary != null && b.Summary.ToLower().Contains(keyword)) ||
                    (b.Tags != null && b.Tags.ToLower().Contains(keyword)))
                .OrderByDescending(b => b.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        // =============== ADD BLOG ===============
        public async Task AddBlogAsync(Blog blog)
        {
            blog.CreatedAt = DateTime.UtcNow;
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        // =============== UPDATE BLOG ===============
        public async Task UpdateBlogAsync(Blog blog)
        {
            var existing = await _context.Blogs.FindAsync(blog.BlogId);
            if (existing == null) return;

            existing.Title = blog.Title;
            existing.Slug = blog.Slug;
            existing.Summary = blog.Summary;
            existing.Content = blog.Content;
            existing.ThumbnailUrl = blog.ThumbnailUrl;
            existing.Tags = blog.Tags;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        // =============== DELETE BLOG ===============
        public async Task DeleteBlogAsync(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }
}
