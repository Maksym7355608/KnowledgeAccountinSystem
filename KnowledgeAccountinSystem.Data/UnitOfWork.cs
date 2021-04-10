using KnowledgeAccountinSystem.Data.Interfaces;
using KnowledgeAccountinSystem.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly KnowledgeAccountinSystemContext context;
        private AccountRepository account;
        private ProgrammerRepository programmer;
        private ManagerRepository manager;
        private SkillRepository skill;

        public UnitOfWork(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public IAccountRepository AccountRepository => account 
            ??= new AccountRepository(context);

        public ISkillRepository SkillRepository => skill 
            ??= new SkillRepository(context);
        
        public IProgrammerRepository ProgrammerRepository => programmer 
            ??= new ProgrammerRepository(context);

        public IManagerRepository ManagerRepository => manager 
            ??= new ManagerRepository(context);

        public async Task SaveAsync() => await context
            .SaveChangesAsync();
        public void Save() => context
            .SaveChanges();


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
