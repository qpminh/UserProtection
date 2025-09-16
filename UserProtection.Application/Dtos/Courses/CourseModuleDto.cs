namespace UserProtection.Application.Dtos.Courses
{
    public class CourseModuleDto
    {
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int OrderIndex { get; set; }
    }

    public class CreateCourseModuleRequest
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int OrderIndex { get; set; }
    }

    public class UpdateCourseModuleRequest
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int OrderIndex { get; set; }
    }
}
