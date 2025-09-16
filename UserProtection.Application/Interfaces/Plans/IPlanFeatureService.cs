namespace UserProtection.Application.Interfaces.Plans
{
    public interface IPlanFeatureService
    {
        Task AddFeatureToPlanAsync(int planId, int featureId);
        Task RemoveFeatureFromPlanAsync(int planId, int featureId);
    }
}
