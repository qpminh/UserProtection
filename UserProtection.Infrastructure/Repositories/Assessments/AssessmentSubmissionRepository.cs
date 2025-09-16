using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Infrastructure.Repositories.Assessments
{
    public class AssessmentSubmissionRepository : IAssessmentSubmissionRepository
    {
        private readonly UserProtectionContext _context;
        public AssessmentSubmissionRepository(UserProtectionContext context) => _context = context;

        public async Task<AssessmentSubmission?> GetByIdAsync(int id) =>
            await _context.AssessmentSubmissions
                .Include(s => s.Assessment)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SubmissionId == id);

        public async Task<IEnumerable<AssessmentSubmission>> GetByAssessmentAsync(int assessmentId) =>
            await _context.AssessmentSubmissions
                .Where(s => s.AssessmentId == assessmentId)
                .Include(s => s.User)
                .ToListAsync();

        public async Task<IEnumerable<AssessmentSubmission>> GetByUserAsync(string userId) =>
            await _context.AssessmentSubmissions
                .Where(s => s.UserId == userId)
                .Include(s => s.Assessment)
                .ToListAsync();

        public async Task AddAsync(AssessmentSubmission entity) =>
            await _context.AssessmentSubmissions.AddAsync(entity);

        public void Update(AssessmentSubmission entity) =>
            _context.AssessmentSubmissions.Update(entity);

        public void Delete(AssessmentSubmission entity) =>
            _context.AssessmentSubmissions.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
