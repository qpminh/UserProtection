using UserProtection.Application.Dtos.Plan;

namespace UserProtection.Application.Interfaces.Plans
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanDto>> GetActivePlansAsync();
        Task<IEnumerable<PlanDto>> GetAllPlansAsync();
        Task<PlanDto?> GetPlanWithCoursesAsync(int planId);
        Task<PlanDto?> GetPlanWithFeaturesAsync(int planId);
        Task<PlanDto?> GetPlanDetailsAsync(int planId);
    }
}
