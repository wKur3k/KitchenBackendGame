using AutoMapper;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(m => m.Login, c => c.MapFrom(s => s.User.Login));
            CreateMap<User, UserDto>();
            CreateMap<Hero, HeroDto>();
        }
    }
}
