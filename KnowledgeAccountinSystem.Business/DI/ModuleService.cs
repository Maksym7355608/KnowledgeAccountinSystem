using AutoMapper;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Mapper;
using KnowledgeAccountinSystem.Business.Services;
using KnowledgeAccountinSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeAccountinSystem.Business.DI
{
    public static class ModuleService
    {
        public static void InjectionDependencyCustomServices(this IServiceCollection services, string connectionString)
        {
            //injection data access layer
            services.AddDbContext<KnowledgeAccountinSystemContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //injection business logic layer
            services.AddSingleton<IMapper>(new AutoMapper.Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile<AutomapperProfile>())));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProgrammerService, ProgrammerService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IStatisticService, StatisticService>();
        }
    }
}