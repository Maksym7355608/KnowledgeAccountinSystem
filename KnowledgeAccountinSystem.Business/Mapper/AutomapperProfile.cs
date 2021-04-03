using AutoMapper;
using KnowledgeAccountinSystem.API.Models;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountinSystem.Business.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Programmer, ProgrammerModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.User.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(z => z.User.Surname))
                .ForMember(x => x.Skills, y => y.MapFrom(z => z.Skills))
                .ReverseMap();

            CreateMap<Manager, ManagerModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.User.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(z => z.User.Surname))
                .ForMember(x => x.Programmers, y => y.MapFrom(z => z.Programmers))
                .ReverseMap();
        }
    }
}
