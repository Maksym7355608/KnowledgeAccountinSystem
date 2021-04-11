using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAccountChanger
    {
        /// <summary>
        /// This method update user account
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ModelException">model incorrect</exception>
        /// <returns></returns>
        Task UpdateAccountAsync(UserModel model);

        /// <summary>
        /// This method delete user account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteAccountAsync(int userId);

        /// <summary>
        /// This method get id by role
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="AuthorizeException">id by role not found</exception>
        /// <returns></returns>
        int GetRoleId(int userId);
    }
}
