using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Models;
using Assignment3.DTOs.Character;

namespace Assignment3.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            // Character<->CharacterReadDTO
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(pdto => pdto.Movies, opt =>
                opt.MapFrom(p => p.Movie.Select(s => s.MovieId).ToArray()))
                .ReverseMap();
            // Character<->CharacterCreateDTO
            CreateMap<Character, CharacterCreateDTO>()
                .ReverseMap();
            // Character<->CharacterUpdateDTO
            CreateMap<Character, CharacterUpdateDTO>()
                .ReverseMap();
        }
    }
}
