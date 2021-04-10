using KnowledgeAccountinSystem.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IAdminService
    {
        Task ChangeRoleAsync(int userId);
        IEnumerable<UserModel> GetUsers();
    }
}
