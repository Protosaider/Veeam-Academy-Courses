using System;
using ClientApp.DataSuppliers.Data;

namespace ClientApp.DataSuppliers
{
	internal interface IUserSupplier
    {
        Boolean UpdateActivityStatus(Guid userId, Int32 currentStatus);
        Boolean UpdateLastActiveDate(Guid userId, DateTimeOffset currentDate);
        CUserData GetUserData(Guid userId);
    }
}
