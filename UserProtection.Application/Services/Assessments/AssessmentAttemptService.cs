using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Application.Services.Assessments
{
    public class AssessmentAttemptService : IAssessmentAttemptService
    {
        private readonly IAssessmentAttemptRepository _attemptRepo;
        private readonly IAssessmentAnswerRepository _answerRepo;
        private readonly IAssessmentRepository _assessmentRepo;
        private readonly IMapper _mapper;

        public AssessmentAttemptService(
            IAssessmentAttemptRepository attemptRepo,
            IAssessmentAnswerRepository answerRepo,
            IAssessmentRepository assessmentRepo,
            IMapper mapper)
        {
            _attemptRepo = attemptRepo;
            _answerRepo = answerRepo;
            _assessmentRepo = assessmentRepo;
            _mapper = mapper;
        }

        public async Task<AssessmentAttemptDto> StartAttemptAsync(CreateAssessmentAttemptDto dto)
        {
            var assessment = await _assessmentRepo.GetByIdAsync(dto.AssessmentId)
                ?? throw new KeyNotFoundException("Assessment not found");

            var attemptNumber = await _attemptRepo.GetNextAttemptNumberAsync(dto.AssessmentId, dto.UserId);

            var attempt = new AssessmentAttempt
            {
                AssessmentId = dto.AssessmentId,
                UserId = dto.UserId,
                StartedAt = DateTime.UtcNow,
                AttemptNumber = attemptNumber
            };

            await _attemptRepo.AddAsync(attempt);
            await _attemptRepo.SaveChangesAsync();

            // Map answers
            var answers = dto.Answers.Select(a => new AssessmentAnswer
            {
                AttemptId = attempt.AttemptId,
                QuestionId = a.QuestionId,
                OptionId = a.OptionId,
                AnswerText = a.AnswerText
            }).ToList();

            await _answerRepo.AddRangeAsync(answers);
            await _attemptRepo.SaveChangesAsync();

            // Chấm điểm nếu trắc nghiệm
            if (assessment.AssessmentType == "MCQ")
            {
                decimal totalScore = 0;
                foreach (var ans in answers)
                {
                    var correctOption = assessment.AssessmentQuestions
                        .FirstOrDefault(q => q.QuestionId == ans.QuestionId)?
                        .AssessmentOptions.FirstOrDefault(o => o.IsCorrect);

                    if (correctOption != null && ans.OptionId == correctOption.OptionId)
                    {
                        ans.IsCorrect = true;
                        totalScore += assessment.AssessmentQuestions
                            .First(q => q.QuestionId == ans.QuestionId).Points;
                    }
                    else
                    {
                        ans.IsCorrect = false;
                    }
                }

                attempt.Score = totalScore;
                attempt.CompletedAt = DateTime.UtcNow;
                _attemptRepo.Update(attempt);
                await _attemptRepo.SaveChangesAsync();
            }

            return _mapper.Map<AssessmentAttemptDto>(attempt);
        }

        public async Task<AssessmentAttemptDto?> GetAttemptAsync(int id)
        {
            var attempt = await _attemptRepo.GetByIdAsync(id);
            return _mapper.Map<AssessmentAttemptDto?>(attempt);
        }
    }
}
