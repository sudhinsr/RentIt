﻿using Microsoft.EntityFrameworkCore;
using RentIt.Models;
using RentIt.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace RentIt.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
            where T : class, new()
    {
        protected RentItContext _dbContext { get; set; }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _dbContext.Remove(_dbContext.Find<T>(id));
            await _dbContext.SaveChangesAsync();
        }
    }
}
    