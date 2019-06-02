using System;
using System.Data;
using Info;

namespace DataStorage.Mappers
{
    public class CMessageInChatInfoMapper : IMapper<CMessageInChatInfo>
    {
        public CMessageInChatInfo ReadItem(IDataReader reader)
        {
            return new CMessageInChatInfo(
                (Guid)reader["Id"],
                (Guid)reader["MessageId"],
                (Guid)reader["ChatId"],
                (Guid)reader["FromUserId"],
                (Guid)reader["ToUserId"],
                (Boolean)reader["IsRead"]
                );
        }
    }
}