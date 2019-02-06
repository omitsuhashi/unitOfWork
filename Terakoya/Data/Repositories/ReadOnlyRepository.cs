using Microsoft.EntityFrameworkCore;
using Terakoya.Data.Repositories.Interfaces;

namespace Terakoya.Data.Repositories
{
    public class ReadOnlyRepository<T> : BaseRepository<T>, IReadRepository<T> where T : class
    {
        public ReadOnlyRepository(DbContext context) : base(context)
        {
        }
    }
}
