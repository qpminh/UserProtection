using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Infrastructure.Repositories.Assessments
{
    public class AssessmentOptionRepository : IAssessmentOptionRepository
    {
        private readonly UserProtectionContext _context;
        public AssessmentOptionRepository(UserProtectionContext context) => _context = context;

        public async Task<AssessmentOption?> GetByIdAsync(int id) =>
            await _context.AssessmentOptions
                .FirstOrDefaultAsync(o => o.OptionId == id);

        public async Task<IEnumerable<AssessmentOption>> GetByQuestionAsync(int questionId) =>
            await _context.AssessmentOptions
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();

        public async Task AddAsync(AssessmentOption entity) =>
            await _context.AssessmentOptions.AddAsync(entity);

        public void Update(AssessmentOption entity) =>
            _context.AssessmentOptions.Update(entity);

        public void Delete(AssessmentOption entity) =>
            _context.AssessmentOptions.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
