using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Infrastructure.Repositories.Assessments
{
    public class AssessmentAttemptRepository : IAssessmentAttemptRepository
    {
        private readonly UserProtectionContext _context;
        public AssessmentAttemptRepository(UserProtectionContext context) => _context = context;

        public async Task<AssessmentAttempt?> GetByIdAsync(int id) =>
            await _context.AssessmentAttempts
                .Include(a => a.AssessmentAnswers)
                .FirstOrDefaultAsync(a => a.AttemptId == id);

        public async Task<IEnumerable<AssessmentAttempt>> GetByAssessmentAsync(int assessmentId, string userId) =>
            await _context.AssessmentAttempts
                .Where(a => a.AssessmentId == assessmentId && a.UserId == userId)
                .ToListAsync();

        public async Task<int> GetNextAttemptNumberAsync(int assessmentId, string userId)
        {
            var last = await _context.AssessmentAttempts
                .Where(a => a.AssessmentId == assessmentId && a.UserId == userId)
                .OrderByDescending(a => a.AttemptNumber)
                .FirstOrDefaultAsync();

            return last == null ? 1 : last.AttemptNumber + 1;
        }

        public async Task AddAsync(AssessmentAttempt entity) =>
            await _context.AssessmentAttempts.AddAsync(entity);

        public void Update(AssessmentAttempt entity) =>
            _context.AssessmentAttempts.Update(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
