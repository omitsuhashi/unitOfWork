namespace Terakoya.Data.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
        IReadRepository<T> GetReadOnlyRepository<T>() where T : class;
    }
}
