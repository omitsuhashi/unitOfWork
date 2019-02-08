using Microsoft.EntityFrameworkCore;
using Terakoya.Data.Repositories.Interfaces;
using Terakoya.Models;

namespace Terakoya.Data.Repositories
{
    public class ReadOnlyRepository<T> : BaseRepository<T>, IReadRepository<T> where T : ModelBase
    {
        public ReadOnlyRepository(DbContext context) : base(context)
        {
        }
    }
}
