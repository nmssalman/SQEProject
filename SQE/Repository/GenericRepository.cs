﻿using Microsoft.EntityFrameworkCore;
using SQE.Data;
using SQE.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SQE.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(DatabaseContext context)
        {
            this._context = context;
            _db = _context.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRage(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRage(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
