using KnowledgeAccountinSystem.Business.Models;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAccountService
    {
        Task<string> LogInAsync(string email, string password);
        Task RegisterAsync(UserModel model);
        Task UpdateAccountAsync(UserModel model);
        Task DeleteAccountAsync(UserModel model);
        Task ChangeRoleToManagerAsync(UserModel model);
    }
}
