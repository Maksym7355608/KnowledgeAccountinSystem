using KnowledgeAccountinSystem.Data.Entities;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Interfaces
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
