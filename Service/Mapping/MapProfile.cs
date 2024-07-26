using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCreateRequest, User>();
            CreateMap<UserUpdateRequest, User>();
        }
    }
}
