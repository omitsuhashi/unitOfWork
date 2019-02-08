using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Terakoya.Data.Paging;
using Terakoya.Models;

namespace Terakoya.Data.Repositories.Interfaces
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : ModelBase
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(params T[] entities);
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        void Delete(T entity);
        void Delete(object id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}
