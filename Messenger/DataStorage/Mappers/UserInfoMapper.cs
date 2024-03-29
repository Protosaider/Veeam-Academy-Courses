﻿using System;
using System.Data;
using Info;

namespace DataStorage.Mappers
{
	internal sealed class CUserInfoMapper : IMapper<CUserInfo>
    {
        public CUserInfo ReadItem(IDataReader reader)
        {
            return new CUserInfo(
                (Guid)reader["Id"],
                (String)reader["Login"],
                (String)reader["Password"],
                (DateTimeOffset)reader["LastActiveDate"],
                (Int32)(Byte)reader["ActivityStatus"], //! Смотри что и куда кастуешь - в БД - tinyint = Byte
                (String)reader["Avatar"]
                );
        }
    }
}
