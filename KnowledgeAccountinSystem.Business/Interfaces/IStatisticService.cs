using KnowledgeAccountinSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using KnowledgeAccountinSystem.Business.Validation;

namespace KnowledgeAccountinSystem.Business.Interfaces
{
    public interface IStatisticService
    {
        /// <summary>
        /// This method get the most popular programmers by managers count
        /// </summary>
        /// <param name="count"></param>
        /// <exception cref="KASException"/>
        /// <returns></returns>
        IEnumerable<ProgrammerModel> GetTheMostPopularProgrammers(int count);

        /// <summary>
        /// This method get the most popular managers by programmers count
        /// </summary>
        /// <param name="count"></param>
        /// <exception cref="KASException"/>
        /// <returns></returns>
        IEnumerable<ManagerModel> GetTopManagers(int count);

        /// <summary>
        /// This method get the most popular programmers skills
        /// </summary>
        /// <param name="count"></param>
        /// <exception cref="KASException"/>
        /// <returns></returns>
        IEnumerable<Data.Entities.SkillName> GetTheMostPopularSkills(int count);
    }
}
