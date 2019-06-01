using System;
using System.Collections.Generic;
using Info;

namespace DataStorage.DataProviders
{
    public interface ICContactInfoDataProvider
    {
        IList<CContactInfo> GetAllContactsByOwnerId(Guid ownerId);
        Int32 CreateContact(Guid ownerId, Guid userId);
        Int32 DeleteContact(Guid ownerId, Guid userId);
        IList<CContactInfo> GetHasDialogContacts(Guid ownerId);
    }
}