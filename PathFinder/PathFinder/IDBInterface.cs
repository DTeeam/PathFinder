using SQLite;

namespace PathFinder
{
    public interface IDBInterface
    {
        SQLiteAsyncConnection CreateConnection();
    }
}