namespace ControllersAPI.Repos
{
    public interface IRepo<T>
    {
        IEnumerable<T> GetAll { get; }

        T Get(int id);

        Task<T> GetAsync(int id);

        T Post(T entity);

        Guid Guid { get; init; }
    }
}