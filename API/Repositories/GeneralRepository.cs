﻿using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
    where TEntity : class
    {
        protected readonly BookingDbContext _context;

        public GeneralRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>()
                           .ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public TEntity? Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>()
                        .Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>()
                        .Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Clear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
