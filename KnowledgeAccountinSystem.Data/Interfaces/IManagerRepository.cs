using KnowledgeAccountinSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Interfaces
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Task ChooseProgrammerAsync(int id, int programmerId);
        Task<IEnumerable<Programmer>> GetChoosenProgrammersAsync(int id);
        Task DeleteProgrammerAsync(int id, int programmerId);
    }
}
