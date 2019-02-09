using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Terakoya.Data.Repositories;
using Terakoya.Data.Repositories.Interfaces;
using Terakoya.Models;

namespace Terakoya.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : ModelBase;
        IReadRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : ModelBase;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }

    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;
        private readonly string _userId;

        public UnitOfWork(
            TContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? throw new UnauthorizedAccessException();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : ModelBase
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context, _userId);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IReadRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : ModelBase
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new ReadOnlyRepository<TEntity>(Context);
            return (IReadRepository<TEntity>)_repositories[type];
        }

        public TContext Context { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
