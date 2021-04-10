using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// This method change user role
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="NullReferenceException">incorrect id</exception>
        /// <exception cref="KASException">role id incorrect</exception>
        /// <returns></returns>
        Task ChangeRoleAsync(int userId);

        /// <summary>
        /// This method get all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetUsers();
    }
}
