﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task AddAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
    }
}
