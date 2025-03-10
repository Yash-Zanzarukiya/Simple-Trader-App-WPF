﻿using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework.Services.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly ApplicationDBContextFactory _contextFactory;

        public NonQueryDataService(ApplicationDBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            var createdEntity = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return createdEntity.Entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            using ApplicationDBContext context = _contextFactory.CreateDbContext();

            T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null) return false;

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
