using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
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
        /// <exception cref="AuthorizeException">Unauthorized user</exception>
        /// <returns>Token</returns>
        Task<string> LogInAsync(string email, string password);

        /// <summary>
        /// This method add new user as programmer
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="UnuniqueException">exist email</exception>
        /// <exception cref="ModelException">uncorrect register model</exception>
        /// <returns></returns>
        Task RegisterAsync(UserModel model);
    }
}
