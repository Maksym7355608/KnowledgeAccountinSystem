﻿using AutoMapper;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using KnowledgeAccountinSystem.Data;
using KnowledgeAccountinSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public StatisticService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<ProgrammerModel> GetTheMostPopularProgrammers(int count)
        {
            try
            {
                return mapper.Map<IEnumerable<ProgrammerModel>>(context.ProgrammerRepository
                     .GetAll()
                     .OrderByDescending(x => x)
                     .Take(count));
            }
            catch (KASException)
            {
                throw new KASException();
            }
        }

        public IEnumerable<SkillName> GetTheMostPopularSkills(int count)
        {
            try
            {
                var programmers = context.ProgrammerRepository.GetAll();

                var skills = programmers
                    .Select(x => x.Skills
                    .Select(y => y.Name));

                var result = skills
                    .Aggregate((current, next) => current.Concat(next))
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count())
                    .Select(x => x.Key)
                    .Take(count);

                return result;
            }
            catch (KASException)
            {
                throw new KASException();
            }

        }

        public IEnumerable<ManagerModel> GetTopManagers(int count)
        {
            try
            {
                return mapper.Map<IEnumerable<ManagerModel>>(context.ManagerRepository
                .GetAll()
                .OrderByDescending(x => x.Programmers.Count())
                .Take(count));
            }
            catch (KASException)
            {
                throw new KASException();
            }
        }
    }
}