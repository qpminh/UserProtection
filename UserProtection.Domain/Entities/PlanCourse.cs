namespace UserProtection.Domain.Entities;

public partial class PlanCourse
{
    public int PlanId { get; set; }
    public int CourseId { get; set; }

    public virtual Plan Plan { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
