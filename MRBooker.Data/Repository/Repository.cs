﻿using Microsoft.EntityFrameworkCore;
using MRBooker.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRBooker.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> entities;

        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Insert failed: entity");
            }
            entities.Add(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Update failed: entity");
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Delete failed: entity");
            }
            entities.Remove(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }
    }
}
