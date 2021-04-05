using KnowledgeAccountinSystem.Data.Interfaces;
using KnowledgeAccountinSystem.Data.Repositories;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KnowledgeAccountinSystemContext context;

        public UnitOfWork(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public IAccountRepository AccountRepository => new AccountRepository(context);

        public ISkillRepository SkillRepository => new SkillRepository(context);

        public IProgrammerRepository ProgrammerRepository => new ProgrammerRepository(context);

        public IManagerRepository ManagerRepository => new ManagerRepository(context);

        public async Task SaveAsync() => await context.SaveChangesAsync();
        public void Save() => context.SaveChanges();
    }
}
