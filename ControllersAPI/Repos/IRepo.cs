namespace ControllersAPI.Repos
{
    public interface IRepo<T>
    {
        IEnumerable<T> GetAll { get; }

        T Get(int id);

        Task<T> GetAsync(int id);
    }
}