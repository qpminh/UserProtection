using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Infrastructure.Repositories.Assessments
{
    public class AssessmentAnswerRepository : IAssessmentAnswerRepository
    {
        private readonly UserProtectionContext _context;
        public AssessmentAnswerRepository(UserProtectionContext context) => _context = context;

        public async Task<AssessmentAnswer?> GetByIdAsync(int id) =>
            await _context.AssessmentAnswers
                .Include(a => a.Question)
                .Include(a => a.Option)
                .FirstOrDefaultAsync(a => a.AnswerId == id);

        public async Task<IEnumerable<AssessmentAnswer>> GetByAttemptAsync(int attemptId) =>
            await _context.AssessmentAnswers
                .Where(a => a.AttemptId == attemptId)
                .Include(a => a.Question)
                .Include(a => a.Option)
                .ToListAsync();

        public async Task AddAsync(AssessmentAnswer entity) =>
            await _context.AssessmentAnswers.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<AssessmentAnswer> entities) =>
            await _context.AssessmentAnswers.AddRangeAsync(entities);

        public void Update(AssessmentAnswer entity) =>
            _context.AssessmentAnswers.Update(entity);

        public void Delete(AssessmentAnswer entity) =>
            _context.AssessmentAnswers.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
