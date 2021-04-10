using KnowledgeAccountinSystem.Business.Models;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// This method authorize users
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Token</returns>
        Task<string> LogInAsync(string email, string password);

        /// <summary>
        /// This method add new user as programmer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task RegisterAsync(UserModel model);
    }
}
