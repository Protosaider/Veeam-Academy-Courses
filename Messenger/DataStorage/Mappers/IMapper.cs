using System.Data;

namespace DataStorage.Mappers
{
    public interface IMapper<out T>
    {
        T ReadItem(IDataReader reader);
    }
}
