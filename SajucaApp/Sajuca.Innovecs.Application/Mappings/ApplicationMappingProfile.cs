using AutoMapper;
using Sajuca.Innovecs.Application.Models;
using Sajuca.Innovecs.Client.Api1.Entities;
using Sajuca.Innovecs.Client.Api1.Models;
using Sajuca.Innovecs.Client.Api2.Entities;
using Sajuca.Innovecs.Client.Api2.Models;
using Sajuca.Innovecs.Client.Api3.Entities;
using Sajuca.Innovecs.Client.Api3.Models;
using System;
using System.Collections.Generic;

namespace Sajuca.Innovecs.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<FirstCompanyQuote, DtoFirstCompanyQuote>().ReverseMap();
            CreateMap<SecondCompanyQuote, DtoSecondCompanyQuote>().ReverseMap();
            CreateMap<ThirdCompanyQuote, DtoThirdCompanyQuote>().ReverseMap();

            CreateMap<IDictionary<string, object>, PackageDimension>()
                    .ForMember(dest => dest.Height, act => act.MapFrom(src => Convert.ToInt32(src["height"].ToString())))
                    .ForMember(dest => dest.Length, act => act.MapFrom(src => Convert.ToInt32(src["length"].ToString())))
                    .ForMember(dest => dest.Weight, act => act.MapFrom(src => Convert.ToInt32(src["weight"].ToString())))
                    .ForMember(dest => dest.Width, act => act.MapFrom(src => Convert.ToInt32(src["width"].ToString())));

            CreateMap<IDictionary<string, object>, Cartons>()
                    .ForMember(dest => dest.Height, act => act.MapFrom(src => Convert.ToInt32(src["height"].ToString())))
                    .ForMember(dest => dest.Length, act => act.MapFrom(src => Convert.ToInt32(src["length"].ToString())))
                    .ForMember(dest => dest.Weight, act => act.MapFrom(src => Convert.ToInt32(src["weight"].ToString())))
                    .ForMember(dest => dest.Width, act => act.MapFrom(src => Convert.ToInt32(src["width"].ToString())));

            CreateMap<IDictionary<string, object>, Package>()
                   .ForMember(dest => dest.Height, act => act.MapFrom(src => Convert.ToInt32(src["height"].ToString())))
                   .ForMember(dest => dest.Length, act => act.MapFrom(src => Convert.ToInt32(src["length"].ToString())))
                   .ForMember(dest => dest.Weight, act => act.MapFrom(src => Convert.ToInt32(src["weight"].ToString())))
                   .ForMember(dest => dest.Width, act => act.MapFrom(src => Convert.ToInt32(src["width"].ToString())));
        }
    }
}
