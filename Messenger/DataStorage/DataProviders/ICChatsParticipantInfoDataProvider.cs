using System;
using Info;

namespace DataStorage.DataProviders
{
    public interface ICChatsParticipantInfoDataProvider
    {
        Int32 CreateChatParticipant(CChatsParticipantInfo chatParticipantInfo);
    }
}