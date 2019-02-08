using Terakoya.Models;

namespace Terakoya.Data.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : ModelBase;
        IReadRepository<T> GetReadOnlyRepository<T>() where T : ModelBase;
    }
}
