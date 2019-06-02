using DataStorage.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataStorage.Other;
using Info;
using log4net;

namespace DataStorage.DataProviders
{
    public class CContactInfoDataProvider : ICContactInfoDataProvider
    {
        private readonly CDataStorageSettings _dbSettings;
        private static readonly ILog s_log = SLogger.GetLogger();

        public CContactInfoDataProvider(CDataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }

        //        #region Static
        //        public static IList<CContactInfo> GetAllContactsByOwnerId(Guid ownerId)
        //        {
        //            #region SQL Query
        //            var sql = @"
        //SELECT 
        //    [Id] = c.Id,
        //    [OwnerId] = c.OwnerId,
        //    [UserId] = c.UserId,
        //    [IsBlocked] = c.IsBlocked
        //FROM contactsLists AS c 
        //    WHERE c.OwnerId = @OwnerId
        //";
        //            #endregion


        //            return CDbQueryExecutor.GetDataParametrized(new CContactInfoMapper(), sql,
        //                SSqlParameterCreator.Create(
        //                    "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    )
        //                );
        //        }

        //        public static Int32 CreateContact(Guid ownerId, Guid userId)
        //        {
        //            #region SQL Query
        //            var sql = @"
        //INSERT INTO contactsLists (Id, OwnerId, UserId, IsBlocked)
        //    VALUES (
        //DEFAULT, @OwnerId, @UserId, 0
        //)
        //";
        //            #endregion

        //            return CDbQueryExecutor.CreateItemParametrized(sql,
        //                SSqlParameterCreator.Create(
        //                    "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    )
        //                );
        //        }

        //        public static Int32 DeleteContact(Guid ownerId, Guid userId)
        //        {
        //            #region SQL Query
        //            var sqlQuery = @"
        //DELETE FROM contactsLists 
        //    WHERE OwnerId = @OwnerId
        //        AND UserId = @UserId
        //";
        //            #endregion

        //            return CDbQueryExecutor.DeleteItemParametrized(sqlQuery,
        //                SSqlParameterCreator.Create(
        //                    "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    )
        //                );
        //        }

        //        public static IList<CContactInfo> GetHasDialogContacts(Guid ownerId)
        //        {
        //            #region SQL Query
        //            var sql = @"
        //SELECT 
        //    [Id] = c.Id,
        //    [OwnerId] = c.OwnerId,
        //    [UserId] = c.UserId,
        //    [IsBlocked] = c.IsBlocked
        //FROM (
        //    SELECT DISTINCT 
        //        p.ChatId
        //    FROM chatsParticipants p 

        //    INNER JOIN chatsParticipants p2 

        //    ON p.ChatId = p2.ChatId
        //        WHERE p.UserId = @OwnerId
        //        GROUP BY p.ChatId
        //        HAVING COUNT(p.ChatId) = 2
        //    ) as res

        //INNER JOIN chatsParticipants p 
        //ON res.ChatId = p.ChatId

        //INNER JOIN contactsLists c
        //ON p.UserId = c.UserId
        //    WHERE p.UserId != @OwnerId
        //    AND c.OwnerId = @OwnerId
        //";
        //            #endregion

        //            return CDbQueryExecutor.GetDataParametrized(new CContactInfoMapper(), sql,
        //                SSqlParameterCreator.Create(
        //                    "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
        //                )
        //            );
        //        } 
        //        #endregion

        public IList<CContactInfo> GetAllContactsByOwnerId(Guid ownerId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetAllContactsByOwnerId)}({ownerId})' is called");

            #region SQL Query
            var sql = @"
SELECT 
    [Id] = c.Id,
    [OwnerId] = c.OwnerId,
    [UserId] = c.UserId,
    [IsBlocked] = c.IsBlocked
FROM contactsLists AS c 
    WHERE c.OwnerId = @OwnerId
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CContactInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetAllContactsByOwnerId)}({ownerId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetAllContactsByOwnerId)}({ownerId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetAllContactsByOwnerId)}({ownerId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        //TODO https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy?redirectedfrom=MSDN&view=netframework-4.7.2
        //TODO https://stackoverflow.com/questions/36815927/inserting-multiple-records-into-sql-server-database-using-for-loop
        //TODO https://stackoverflow.com/questions/8106789/how-to-insert-multiple-rows-into-an-database-in-c-ado-net
        //TODO https://stackoverflow.com/questions/2972974/how-should-i-multiple-insert-multiple-records

        public Int32 CreateContact(Guid ownerId, Guid userId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(CreateContact)}({ownerId}, {userId})' is called");
            
            #region SQL Query
            var sql = @"
INSERT INTO contactsLists (Id, OwnerId, UserId, IsBlocked)
    VALUES (
DEFAULT, @OwnerId, @UserId, 0
)
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.CreateItem(sql,
                            SSqlParameterCreator.Create(
                                "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(CreateContact)}({ownerId}, {userId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(CreateContact)}({ownerId}, {userId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(CreateContact)}({ownerId}, {userId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        public Int32 DeleteContact(Guid ownerId, Guid userId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(DeleteContact)}({ownerId}, {userId})' is called");
            
            #region SQL Query
            var sqlQuery = @"
DELETE FROM contactsLists 
    WHERE OwnerId = @OwnerId
        AND UserId = @UserId
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.DeleteItem(sqlQuery,
                            SSqlParameterCreator.Create(
                                "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(DeleteContact)}({ownerId}, {userId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(DeleteContact)}({ownerId}, {userId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(DeleteContact)}({ownerId}, {userId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        //TODO Just search chats with type = 0 (Dialog)
        public IList<CContactInfo> GetHasDialogContacts(Guid ownerId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(DeleteContact)}({ownerId})' is called");
            
            #region SQL Query
            var sql = @"
SELECT 
    [Id] = c.Id,
    [OwnerId] = c.OwnerId,
    [UserId] = c.UserId,
    [IsBlocked] = c.IsBlocked
FROM (
    SELECT DISTINCT 
        p.ChatId
    FROM chatsParticipants p 

    INNER JOIN chatsParticipants p2 

    ON p.ChatId = p2.ChatId
        WHERE p.UserId = @OwnerId
        GROUP BY p.ChatId
        HAVING COUNT(p.ChatId) = 2
    ) as res

INNER JOIN chatsParticipants p 
ON res.ChatId = p.ChatId

INNER JOIN contactsLists c
ON p.UserId = c.UserId
    WHERE p.UserId != @OwnerId
    AND c.OwnerId = @OwnerId
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CContactInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(DeleteContact)}({ownerId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(DeleteContact)}({ownerId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(DeleteContact)}({ownerId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }
    }
}
