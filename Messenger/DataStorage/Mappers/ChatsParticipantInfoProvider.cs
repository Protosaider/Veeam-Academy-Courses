using System;
using System.Data;
using Info;

namespace DataStorage.Mappers
{
    public class CChatsParticipantInfoProvider : IMapper<CChatsParticipantInfo>
    {
        public CChatsParticipantInfo ReadItem(IDataReader reader)
        {
            return new CChatsParticipantInfo(
                (Guid)reader["Id"],
                (Guid)reader["ChatId"],
                (Guid)reader["UserId"]
                );
        }
    }
}