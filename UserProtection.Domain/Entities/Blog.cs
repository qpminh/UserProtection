namespace UserProtection.Domain.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }                // Khóa chính
        public string Title { get; set; } = string.Empty; // Tiêu đề bài viết
        public string Slug { get; set; } = string.Empty;  // Đường dẫn thân thiện (SEO)
        public string Content { get; set; } = string.Empty; // Nội dung HTML hoặc Markdown
        public string? Summary { get; set; }              // Mô tả ngắn
        public string? ThumbnailUrl { get; set; }         // Ảnh đại diện
        public string? Tags { get; set; }                 // Từ khóa, phân loại

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Ngày tạo
        public DateTime? UpdatedAt { get; set; }                    // Ngày cập nhật gần nhất
    }
}
