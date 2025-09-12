using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Dtos.Core;
using UserProtection.Application.Dtos.Security;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<TrustedLink, TrustedLinkDto>().ReverseMap();
            CreateMap<SuspiciousLink, SuspiciousLinkDto>().ReverseMap();
        }
    }
}
