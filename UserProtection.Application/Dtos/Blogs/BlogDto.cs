namespace UserProtection.Application.Dtos.Blogs
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    // Optional: request models
    public class CreateBlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Tags { get; set; }
    }

    public class UpdateBlogRequest
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Content { get; set; }
        public string? Summary { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Tags { get; set; }
    }
}
