using AutoMapper;
using UserProtection.Application.Dtos.Core;
using UserProtection.Application.Dtos.Security;
using UserProtection.Domain.Entities;
using UserProtection.Application.Dtos;

namespace UserProtection.Application.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<TrustedLink, TrustedLinkDto>().ReverseMap();
            CreateMap<SuspiciousLink, SuspiciousLinkDto>().ReverseMap();

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
