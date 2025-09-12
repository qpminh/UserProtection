namespace UserProtection.Domain.Entities
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();
    }
}
