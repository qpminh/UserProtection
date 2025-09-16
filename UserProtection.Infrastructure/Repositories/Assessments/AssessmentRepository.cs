using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Infrastructure.Repositories.Assessments
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly UserProtectionContext _context;
        public AssessmentRepository(UserProtectionContext context) => _context = context;

        public async Task<Assessment?> GetByIdAsync(int id) =>
            await _context.Assessments
                .Include(a => a.AssessmentQuestions)
                .ThenInclude(q => q.AssessmentOptions)
                .FirstOrDefaultAsync(a => a.AssessmentId == id);

        public async Task<IEnumerable<Assessment>> GetByCourseAsync(int courseId) =>
            await _context.Assessments
                .Where(a => a.CourseId == courseId)
                .ToListAsync();

        public async Task AddAsync(Assessment entity) =>
            await _context.Assessments.AddAsync(entity);

        public void Update(Assessment entity) =>
            _context.Assessments.Update(entity);

        public void Delete(Assessment entity) =>
            _context.Assessments.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
