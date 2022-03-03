using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Models;
using Assignment3.DTOs.Franchise;

namespace Assignment3.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            // Franchise<->FranchiseCreateDTO
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(pdto => pdto.Movies, opt =>
                opt.MapFrom(p => p.Movie.Select(s => s.MovieId).ToArray()))
                .ReverseMap();
            // Franchise<->FranchiseCreateDTO
            CreateMap<Franchise, FranchiseCreateDTO>()
                .ReverseMap();
            // Franchise<->FranchiseUpdateDTO
            CreateMap<Franchise, FranchiseUpdateDTO>()
                .ReverseMap();
        }
    }
}
