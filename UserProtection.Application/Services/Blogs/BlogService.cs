using AutoMapper;
using UserProtection.Application.Dtos.Blogs;
using UserProtection.Application.Interfaces.Blogs;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Blogs;

namespace UserProtection.Application.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync() =>
            _mapper.Map<IEnumerable<BlogDto>>(await _repo.GetAllBlogsAsync());

        public async Task<IEnumerable<BlogDto>> GetRecentBlogsAsync(int count = 5) =>
            _mapper.Map<IEnumerable<BlogDto>>(await _repo.GetRecentBlogsAsync(count));

        public async Task<BlogDto?> GetBlogByIdAsync(int blogId) =>
            _mapper.Map<BlogDto?>(await _repo.GetBlogByIdAsync(blogId));

        public async Task<BlogDto?> GetBlogBySlugAsync(string slug) =>
            _mapper.Map<BlogDto?>(await _repo.GetBlogBySlugAsync(slug));

        public async Task<IEnumerable<BlogDto>> SearchBlogsAsync(string keyword) =>
            _mapper.Map<IEnumerable<BlogDto>>(await _repo.SearchBlogsAsync(keyword));

        public async Task<BlogDto> AddBlogAsync(CreateBlogRequest request)
        {
            var entity = _mapper.Map<Blog>(request);
            await _repo.AddBlogAsync(entity);
            return _mapper.Map<BlogDto>(entity);
        }

        public async Task<BlogDto?> UpdateBlogAsync(int blogId, UpdateBlogRequest request)
        {
            var existing = await _repo.GetBlogByIdAsync(blogId);
            if (existing == null) return null;

            // Map chỉ các field được gửi lên (giống behavior PartialUpdate)
            _mapper.Map(request, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateBlogAsync(existing);
            return _mapper.Map<BlogDto>(existing);
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            var existing = await _repo.GetBlogByIdAsync(blogId);
            if (existing == null) return false;

            await _repo.DeleteBlogAsync(blogId);
            return true;
        }
    }
}
