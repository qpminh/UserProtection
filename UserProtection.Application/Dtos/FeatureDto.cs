namespace UserProtection.Application.Dtos;

public class FeatureDto
{
    public int FeatureId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class CreateFeatureRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}