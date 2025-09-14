using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface IPlanRepository
{
    Task<IEnumerable<Plan>> GetActivePlansAsync();
    Task<IEnumerable<Plan>> GetAllPlansAsync();
    Task<Plan?> GetPlanWithCoursesAsync(int planId);
    Task<Plan?> GetPlanWithFeaturesAsync(int planId);
    Task<Plan?> GetPlanDetailsAsync(int planId);
}