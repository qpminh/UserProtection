using AutoMapper;
using UserProtection.Application.Dtos.Core;
using UserProtection.Application.Dtos.Course;
using UserProtection.Application.Dtos.Feature;
using UserProtection.Application.Dtos.Plan;
using UserProtection.Application.Dtos.Security;
using UserProtection.Application.Dtos.Subscription;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Map
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
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CreateCourseRequest, Course>();

            // Plan
            CreateMap<Plan, PlanDto>()
                .ForMember(dest => dest.Courses,
                    opt => opt.MapFrom(src => src.PlanCourses.Select(pc => pc.Course)))
                .ForMember(dest => dest.Features,
                    opt => opt.MapFrom(src => src.PlanFeatures.Select(pf => pf.Feature)));
            CreateMap<CreatePlanRequest, Plan>();

            // Subscription
            CreateMap<Subscription, SubscriptionDto>();
            CreateMap<CreateSubscriptionRequest, Subscription>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.AutoRenew, opt => opt.MapFrom(_ => false));
        }
    }
}
