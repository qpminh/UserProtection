using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync();
        Task<Plan?> GetPlanWithCoursesAsync(int planId);
        Task<Plan?> GetPlanWithFeaturesAsync(int planId);
    }
}
