using System;
using System.Data;
using Info;

namespace DataStorage.Mappers
{
	internal sealed class CChatInfoMapper : IMapper<CChatInfo>
    {
        public CChatInfo ReadItem(IDataReader reader)
        {
            return new CChatInfo(
                (Guid)reader["Id"],
                (String)reader["Title"],
                (Guid)reader["OwnerId"],
                (Boolean)reader["IsPersonal"],
                (Int32)(Byte)reader["Type"]
            );
        }
    }
}
