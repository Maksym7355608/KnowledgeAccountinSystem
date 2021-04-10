using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly KnowledgeAccountinSystemContext context;

        public AccountRepository(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(User entity) => await context.Users
            .AddAsync(entity);

        public async Task DeleteByIdAsync(int id) => context.Users
            .Remove(await context.Users
            .FindAsync(id));

        public IQueryable<User> GetAll() => context.Users
            .AsNoTracking();

        public async Task<User> GetByIdAsync(int id) => await context.Users
            .FindAsync(id);

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password) => await Task.Run(() => 
            context.Users
            .FirstOrDefault(x => x.Email == email && x.Password == password));

        public void Update(User entity) => context.Users
            .Update(entity);
    }
}
