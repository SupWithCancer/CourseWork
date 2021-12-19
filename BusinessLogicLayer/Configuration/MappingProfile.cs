using AutoMapper;
using DataAccessLayer.Models;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UserInfoDTO>();
            CreateMap<UserInfoDTO, User>();

            CreateMap<Film, FilmDTO>();
            CreateMap<FilmDTO, Film>();
        }
    }
}
