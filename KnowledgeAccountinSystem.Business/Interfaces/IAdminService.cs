using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// This method change user role
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="AuthorizeException">incorrect id</exception>
        /// <exception cref="UnuniqueException">admin must by 1</exception>
        /// <exception cref="ModelException">incorrect choosen id</exception>
        /// <returns></returns>
        Task ChangeRoleAsync(int userId);

        /// <summary>
        /// This method get all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetUsers();
    }
}
