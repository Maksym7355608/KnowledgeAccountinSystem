using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
    }
}
