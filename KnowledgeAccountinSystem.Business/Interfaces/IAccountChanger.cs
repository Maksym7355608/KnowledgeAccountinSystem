using KnowledgeAccountinSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAccountChanger
    {
        Task UpdateAccountAsync(UserModel model);
        Task DeleteAccountAsync(int userId);
        int GetRoleId(int userId);
    }
}
