using KnowledgeAccountinSystem.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeAccountinSystem.Business.Validation;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IManagerService : IAccountChanger
    {
        /// <summary>
        /// This method get all programer`s
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProgrammerModel> GetAllProgrammers();

        /// <summary>
        /// This method choose programmer by manager
        /// </summary>
        /// <param name="id"></param>
        /// <param name="programmerId"></param>
        /// <exception cref="ModelException">Programmer is not found</exception>
        /// <returns></returns>
        Task ChooseProgrammerAsync(int id, int programmerId);

        /// <summary>
        /// This method get choosen programmers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<ProgrammerModel>> GetChoosenProgrammersAsync(int id);

        /// <summary>
        /// This method get choosen programmer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <exception cref="ModelException">Programmer is not found</exception>
        /// <returns></returns>
        Task<ProgrammerModel> GetChoosenProgrammerAsync(int id, int programmerId);

        /// <summary>
        /// This method delete choosen programmer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <exception cref="ModelException">Programmer is not found</exception>
        /// <returns></returns>
        Task DeleteProgrammerAsync(int id, int programmerId);
    }
}
