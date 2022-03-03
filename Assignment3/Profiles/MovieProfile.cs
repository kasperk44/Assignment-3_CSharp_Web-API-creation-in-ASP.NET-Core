using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Models;
using Assignment3.DTOs.Movie;

namespace Assignment3.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // Movie<->MovieReadDTO
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(pdto => pdto.Characters, opt =>
                opt.MapFrom(p => p.Character.Select(s => s.CharacterId).ToArray()))
                .ReverseMap();
            // Movie<->MovieCreateDTO
            CreateMap<Movie, MovieCreateDTO>()
                .ReverseMap();
            // Movie<->MovieUpdateDTO
            CreateMap<Movie, MovieUpdateDTO>()
                .ReverseMap();
        }
    }
}
