namespace API;

public interface IRepo<T>
{
    IEnumerable<T> Get();
}
