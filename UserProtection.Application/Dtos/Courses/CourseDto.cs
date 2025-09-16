namespace UserProtection.Application.Dtos.Courses;

public class CourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = null!;
}

public class CreateCourseRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public decimal Price { get; set; }
}

public class UpdateCourseRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; } // Publish, Draft, Archived...
}