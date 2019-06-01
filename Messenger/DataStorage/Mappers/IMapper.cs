using System.Data;
using System.Data.SqlClient;

namespace DataStorage.Mappers
{
    public interface IMapper<out T>
    {
        T ReadItem(IDataReader reader);
    }
}
