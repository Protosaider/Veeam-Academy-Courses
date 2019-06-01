using System;
using System.Data.SqlClient;
using System.Data;

namespace DataStorage
{
	internal static class SSqlParameterCreator
    {
        public static SqlParameter Create(String parameterName, Object parameterValue,
            SqlDbType sqlType, Boolean isNullable, ParameterDirection parameterDirection = ParameterDirection.Input, Int32 parameterLength = 0)
        {
            if (parameterName == null)
                throw new ArgumentNullException(nameof(parameterName));

            var parameter = new SqlParameter(parameterName, sqlType);

            if (isNullable && parameterValue == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = parameterValue;
            }

            parameter.IsNullable = isNullable;
            parameter.SqlDbType = sqlType;
            parameter.Direction = parameterDirection;
              
            if (parameterLength > 0)
                parameter.Size = parameterLength;

            return parameter;
        }
    }
}