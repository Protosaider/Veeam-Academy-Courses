﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientApp.DataSuppliers.Data;

namespace ClientApp.DataSuppliers
{
	internal interface IContactsSupplier
    {
        Task<String> AddContact(Guid ownerId, Guid userId);
        String DeleteContact(Guid ownerId, Guid userId);
        IReadOnlyCollection<CContactData> GetContacts(Guid ownerId);
        IReadOnlyCollection<Guid> GetHasDialogContactsId(Guid ownerId);
        IReadOnlyCollection<CContactData> FindContacts(Guid id, String lastSearchText);
        IReadOnlyDictionary<Guid, DateTimeOffset> GetContactsLastActiveDate(Guid ownerId);
    }
}
