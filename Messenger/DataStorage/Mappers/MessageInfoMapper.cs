﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Info;

namespace DataStorage.Mappers
{
	internal sealed class CMessageInfoMapper : IMapper<CMessageInfo>
    {
        public CMessageInfo ReadItem(IDataReader reader)
        {
            return new CMessageInfo(
                (Guid)reader["Id"],
                (DateTimeOffset)reader["DispatchDate"],
                (String)reader["MessageText"],
                (Int32)(Byte)reader["Type"],
                (String)reader["ContentUri"],
                (Guid)reader["FromUserId"],
                (Boolean)reader["IsRead"],
                (String)reader["Login"],
                (Int64)reader["USN"]
                );
        }
    }
}
