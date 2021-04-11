using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IProgrammerService : IAccountChanger
    {

        /// <summary>
        /// This method get all programer`s skills
        /// </summary>
        /// <param name="programmerId"></param>
        /// <returns></returns>
        Task<IEnumerable<SkillModel>> GetSkillsAsync(int programmerId);

        /// <summary>
        /// This method get programmer`s skill by id
        /// </summary>
        /// <param name="programmerId"></param>
        /// <param name="skillId"></param>
        /// <exception cref="ModelException">incorrect skill id</exception>
        /// <returns></returns>
        Task<SkillModel> GetSkillByIdAsync(int programmerId, int skillId);

        /// <summary>
        /// This method add skill to programmer
        /// </summary>
        /// <param name="programmerId"></param>
        /// <param name="skill"></param>
        /// <exception cref="UnuniqueException">exist skill</exception>
        /// <exception cref="ModelException">incorrect skill model</exception>
        /// <returns></returns>
        Task AddSkillAsync(int programmerId, SkillModel skill);

        /// <summary>
        /// This method delete programmer`s skills
        /// </summary>
        /// <param name="programmerId"></param>
        /// <param name="skillId"></param>
        /// <exception cref="UnuniqueException">skill is not found</exception>
        /// <returns></returns>
        Task DeleteSkillAsync(int programmerId, int skillId);

        /// <summary>
        /// This method update programmer`s skills
        /// </summary>
        /// <param name="programmerId"></param>
        /// <param name="skill"></param>
        /// <exception cref="UnuniqueException">skill is not found</exception>
        /// <exception cref="ModelException">incorrect skill model</exception>
        /// <returns></returns>
        Task EditSkillAsync(int programmerId, SkillModel skill);
    }
}
