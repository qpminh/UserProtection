namespace UserProtection.Application.Dtos;

public class PlanDto
{
    public int PlanId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string BillingCycle { get; set; } = null!;
    public IEnumerable<string> Courses { get; set; } = new List<string>();
    public IEnumerable<string> Features { get; set; } = new List<string>();
}
