using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Infrastructure.Repositories.Assessments
{
    public class AssessmentQuestionRepository : IAssessmentQuestionRepository
    {
        private readonly UserProtectionContext _context;
        public AssessmentQuestionRepository(UserProtectionContext context) => _context = context;

        public async Task<AssessmentQuestion?> GetByIdAsync(int id) =>
            await _context.AssessmentQuestions
                .Include(q => q.AssessmentOptions)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

        public async Task<IEnumerable<AssessmentQuestion>> GetByAssessmentAsync(int assessmentId) =>
            await _context.AssessmentQuestions
                .Where(q => q.AssessmentId == assessmentId)
                .ToListAsync();

        public async Task AddAsync(AssessmentQuestion entity) =>
            await _context.AssessmentQuestions.AddAsync(entity);

        public void Update(AssessmentQuestion entity) =>
            _context.AssessmentQuestions.Update(entity);

        public void Delete(AssessmentQuestion entity) =>
            _context.AssessmentQuestions.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
