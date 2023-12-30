namespace ControllersAPI.Repos
{
    public interface IRepo<T>
    {
        IEnumerable<T> GetAll { get; }
    }
}