using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieStore.Entity;
using MovieStore.Entity.Dto;

namespace MovieStore.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            CreateMap<CreateCastDto, BaseUser>()
       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf("@"))))
       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


            CreateMap<CreateCustomerDto, BaseUser>()
   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf("@"))))
   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateDirectorDto, BaseUser>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf("@"))))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateMovieDto, Movie>().ReverseMap();


        }
    }
}
