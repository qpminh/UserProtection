using AutoMapper;
using UserProtection.Application.Dtos;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // Plan -> PlanDto
            CreateMap<Plan, PlanDto>()
                .ForMember(dest => dest.Courses,
                           opt => opt.MapFrom(src => src.PlanCourses.Select(pc => pc.Course.Title)))
                .ForMember(dest => dest.Features,
                           opt => opt.MapFrom(src => src.PlanFeatures.Select(pf => pf.Feature.Name)));

            // Subscription -> SubscriptionDto
            CreateMap<Subscription, SubscriptionDto>();

            // Request DTO -> Subscription
            CreateMap<CreateSubscriptionRequest, Subscription>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.AutoRenew, opt => opt.MapFrom(_ => false));
        }
    }
}
