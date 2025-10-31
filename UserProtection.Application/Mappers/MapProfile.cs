using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Dtos.Blogs;
using UserProtection.Application.Dtos.Cores;
using UserProtection.Application.Dtos.Courses;
using UserProtection.Application.Dtos.Features;
using UserProtection.Application.Dtos.Plans;
using UserProtection.Application.Dtos.Security;
using UserProtection.Application.Dtos.Subscriptions;
using UserProtection.Application.Dtos.Tenants;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Mappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // User
            CreateMap<UserDto, User>().ReverseMap();

            // Security entities
            CreateMap<TrustedLink, TrustedLinkDto>().ReverseMap();
            CreateMap<SuspiciousLink, SuspiciousLinkDto>().ReverseMap();

            // Feature
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<CreateFeatureRequest, Feature>();

            // Course
            CreateMap<Course, CourseDto>();
            CreateMap<CreateCourseRequest, Course>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Draft")); // default
            CreateMap<UpdateCourseRequest, Course>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<CourseModule, CourseModuleDto>();
            //CreateMap<CreateCourseModuleDto, CourseModule>();
            //CreateMap<UpdateCourseModuleDto, CourseModule>();
            CreateMap<CourseReview, CourseReviewDto>();
            //CreateMap<CreateCourseReviewDto, CourseReview>();
            //CreateMap<UpdateCourseReviewDto, CourseReview>();
            CreateMap<Enrollment, EnrollmentDto>();
            //CreateMap<CreateEnrollmentDto, Enrollment>();
            //CreateMap<UpdateEnrollmentDto, Enrollment>();
            CreateMap<UserCourseProgress, UserCourseProgressDto>();
            CreateMap<CreateUserCourseProgressDto, UserCourseProgress>();
            CreateMap<UpdateUserCourseProgressDto, UserCourseProgress>();

            // Plan
            CreateMap<Plan, PlanDto>()
                .ForMember(dest => dest.Courses,
                    opt => opt.MapFrom(src => src.PlanCourses.Select(pc => pc.Course)))
                .ForMember(dest => dest.Features,
                    opt => opt.MapFrom(src => src.PlanFeatures.Select(pf => pf.Feature)));
            CreateMap<CreatePlanRequest, Plan>();

            // Subscription
            // Subscription
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(dest => dest.Payment,
                    opt => opt.MapFrom(src => src.Payments
                        .OrderByDescending(p => p.PaymentDate)
                        .FirstOrDefault())); // lấy payment mới nhất
            CreateMap<CreateSubscriptionRequest, Subscription>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.AutoRenew, opt => opt.MapFrom(_ => false));

            CreateMap<Payment, PaymentSummaryDto>();

            // Tenant
            CreateMap<Tenant, TenantDto>();
            CreateMap<TenantCreateDto, Tenant>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Active"));
            CreateMap<TenantUpdateDto, Tenant>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // TenantUserAccess
            CreateMap<TenantUserAccess, TenantUserAccessDto>();
            CreateMap<TenantUserAccessCreateDto, TenantUserAccess>()
                .ForMember(dest => dest.AssignedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<TenantUserAccessUpdateDto, TenantUserAccess>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Assessment
            CreateMap<Assessment, AssessmentDto>();
            CreateMap<CreateAssessmentDto, Assessment>();
            CreateMap<UpdateAssessmentDto, Assessment>();

            // Question
            CreateMap<AssessmentQuestion, AssessmentQuestionDto>();
            CreateMap<CreateAssessmentQuestionDto, AssessmentQuestion>();
            CreateMap<UpdateAssessmentQuestionDto, AssessmentQuestion>();

            // Option
            CreateMap<AssessmentOption, AssessmentOptionDto>();
            CreateMap<CreateAssessmentOptionDto, AssessmentOption>();
            CreateMap<UpdateAssessmentOptionDto, AssessmentOption>();

            CreateMap<AssessmentAttempt, AssessmentAttemptDto>();
            CreateMap<AssessmentAnswer, AssessmentAnswerDto>().ReverseMap();

            CreateMap<AssessmentSubmission, AssessmentSubmissionDto>().ReverseMap();
            CreateMap<CreateAssessmentSubmissionDto, AssessmentSubmission>();
            CreateMap<UpdateAssessmentSubmissionDto, AssessmentSubmission>();

            // Blog
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<CreateBlogRequest, Blog>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<UpdateBlogRequest, Blog>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
