using System;
using System.Collections.Generic;
using Info;

namespace DataStorage.DataProviders
{
    public interface ICUserInfoDataProvider
    {
        CUserInfo GetUserByAuthData(String login, String password);
        Int32 UpdateUserLastActiveDate(Guid userId, DateTimeOffset lastActiveDate);
        CUserInfo GetUserData(Guid userId);
        Int32 UpdateUserStatus(Guid userId, Int32 currentStatus);
        IList<CUserInfo> GetAllChatParticipantsByChatId(Guid chatId);
        Guid CreateUser(CUserInfo user);
        Int32 DeleteUser(Guid userId);
        IList<CUserInfo> GetAllNotOfflineUsers();
        IList<CUserInfo> SearchContacts(Guid ownerId, String q);
        IList<CUserInfo> GetContactsLastActiveDate(Guid ownerId);
    }
}