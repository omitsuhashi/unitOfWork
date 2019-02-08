using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Terakoya.Data.Repositories.Interfaces;
using Terakoya.Models;

namespace Terakoya.Data.Repositories
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : ModelBase
    {
        private readonly string _userId;

        public Repository(DbContext context, HttpContext httpContext) : base(context)
        {
            _userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public virtual IQueryable<T> Query(string sql, params object[] parameters) => _dbSet.FromSql(sql, parameters);

        public Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddAsync(UpdateBase(entity), cancellationToken);
        }

        public Task AddAsync(params T[] entities)
        {
            return _dbSet.AddRangeAsync(UpdateBase(entities));
        }

        public Task AddAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddRangeAsync(UpdateBase(entities), cancellationToken);
        }
        public Task AddAsync(T entity)
        {
            return AddAsync(UpdateBase(entity), new CancellationToken());
        }

        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }


        public void Delete(object id)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<T>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null) Delete(entity);
            }
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(UpdateBase(entity));
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(UpdateBase(entities));
        }


        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(UpdateBase(entities));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        private T UpdateBase(T entity)
        {
            var now = DateTime.Now;
            if(entity.CreatedAt == null || entity.CreatedBy == null)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = _userId;
            }
            entity.UpdatedAt = now;
            entity.UpdatedBy = _userId;
            return entity;
        }

        private IEnumerable<T> UpdateBase(params T[] entities)
        {
            return entities.Select(_ => UpdateBase(_));
        }

        private IEnumerable<T>  UpdateBase(IEnumerable<T> entities)
        {
            return entities.Select(_ => UpdateBase(_));
        }
    }
}
