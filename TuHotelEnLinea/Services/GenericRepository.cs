﻿using Microsoft.EntityFrameworkCore;
using TuHotelEnLinea.Data;

namespace TuHotelEnLinea.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected TuHotelEnLineaContext _context;
        internal DbSet<T> _dbSet;
        public GenericRepository(TuHotelEnLineaContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async void Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new Exception($"La entidad con el id {id.ToString()} no existe");

            _dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
