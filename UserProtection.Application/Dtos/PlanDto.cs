namespace UserProtection.Application.Dtos;

public class PlanDto
{
    public int PlanId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string BillingCycle { get; set; } = null!;
    public bool IsActive { get; set; }

    public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
    public IEnumerable<FeatureDto> Features { get; set; } = new List<FeatureDto>();
}

public class CreatePlanRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string BillingCycle { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}
