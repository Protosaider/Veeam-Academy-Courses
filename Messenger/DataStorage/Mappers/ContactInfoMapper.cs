using System;
using System.Data;
using Info;

namespace DataStorage.Mappers
{
	internal sealed class CContactInfoMapper : IMapper<CContactInfo>
    {
        public CContactInfo ReadItem(IDataReader reader)
        {
            return new CContactInfo(
                (Guid)reader["Id"],
                (Guid)reader["OwnerId"],
                (Guid)reader["UserId"],
                (Boolean)reader["IsBlocked"]
                );
        }
    }
}
