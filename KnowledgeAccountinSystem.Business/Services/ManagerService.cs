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
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public ManagerService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task ChooseProgrammerAsync(int id, ProgrammerModel entity)
        {
            if (context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(entity.Id))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            await context.ManagerRepository.ChooseProgrammerAsync(id, mapper.Map<Programmer>(entity));
        }

        public async Task DeleteProgrammerAsync(int id, ProgrammerModel entity)
        {
            if (context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(entity.Id))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            await context.ManagerRepository.DeleteProgrammerAsync(id, mapper.Map<Programmer>(entity));
        }

        public IEnumerable<ProgrammerModel> GetAllProgrammers()
        {
            return mapper.Map<IEnumerable<ProgrammerModel>>(context.ProgrammerRepository.GetAll());
        }

        public async Task<ProgrammerModel> GetChoosenProgrammerAsync(int id, ProgrammerModel entity)
        {
            if (context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(entity.Id))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            IEnumerable<ProgrammerModel> choosen = await GetChoosenProgrammersAsync(id) ?? null;

            return mapper.Map<ProgrammerModel>(choosen.FirstOrDefault(x => x == entity));
        }

        public async Task<IEnumerable<ProgrammerModel>> GetChoosenProgrammersAsync(int id)
        {
            IEnumerable<ProgrammerModel> choosen = mapper.Map<IEnumerable<ProgrammerModel>>
                (await context.ManagerRepository?.GetChoosenProgrammersAsync(id));
            return choosen ?? throw new KASException("no choosen programmers", HttpStatusCode.NotFound);
        }
    }
}
