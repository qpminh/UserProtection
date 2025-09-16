namespace UserProtection.Application.Interfaces.Plans
{
    public interface IPlanCourseService
    {
        Task AddCourseToPlanAsync(int planId, int courseId);
        Task RemoveCourseFromPlanAsync(int planId, int courseId);
    }
}
