using DataStorage.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info;
using log4net;
using Other;

namespace DataStorage.DataProviders
{
    public class CChatInfoDataProvider : ICChatInfoDataProvider
    {
        private readonly CDataStorageSettings _dbSettings;
        private static readonly ILog s_log = SLogger.GetLogger();

        public CChatInfoDataProvider(CDataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }

//        #region Static methods

//        [Obsolete("Use instance method instead of static")]
//        public static IList<CChatInfo> GetChatsByParticipantId(Guid userId)
//        {
//            #region SQL Query
//            var sqlQuery = @"
//SELECT DISTINCT 
//    [Id] = c.Id, 
//    [Title] = c.Title, 
//    [OwnerId] = c.OwnerId, 
//    [IsPersonal] = c.IsPersonal,
//    [Type] = c.Type
//FROM chatsParticipants p 

//INNER JOIN chats c 

//ON p.ChatId = c.Id
//    WHERE p.UserId = @UserId
//";
//            #endregion

//            var result = CDbQueryExecutor.GetDataParametrized(new CChatInfoMapper(), sqlQuery,
//                SSqlParameterCreator.Create(
//                    "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
//                    )
//                );
//            return result;
//        }

//        [Obsolete("Use instance method instead of static")]
//        public static Int32 GetUnreadMessagesCount(Guid userId, Guid chatId)
//        {
//            #region SQL Query
//            var sqlQuery = @"
//SELECT DISTINCT
//    COUNT(*)
//FROM messagesInChats
//    WHERE ChatId = @ChatId
//    AND ToUserId = @UserId
//    AND IsRead = 0
//";
//            #endregion

//            Int32 result = CDbQueryExecutor.GetScalar<Int32>(sqlQuery,
//                SSqlParameterCreator.Create("@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false),
//                SSqlParameterCreator.Create("@ChatId", chatId, System.Data.SqlDbType.UniqueIdentifier, false)
//                );
//            return result;
//        }

//        [Obsolete("Use instance method instead of static")]
//        public static IList<CChatInfo> GetChatsByOwnerId(Guid userId)
//        {
//            #region SQL Query
//            var sqlQuery = @"
//SELECT 
//    [Id] = Id, 
//    [Title] = Title, 
//    [OwnerId] = OwnerId, 
//    [IsPersonal] = IsPersonal,
//    [Type] = Type
//FROM chats AS c 
//    WHERE c.OwnerId = @OwnerId
//";
//            #endregion

//            var result = CDbQueryExecutor.GetData(new CChatInfoMapper(), sqlQuery);
//            return result;
//        }

//        [Obsolete("Use instance method instead of static")]
//        public static CChatInfo GetChatById(Guid chatId)
//        {
//            #region SQL Query
//            var sqlQuery = @"
//SELECT 
//    [Id] = Id, 
//    [Title] = Title, 
//    [OwnerId] = OwnerId, 
//    [IsPersonal] = IsPersonal,
//    [Type] = Type
//FROM chats AS c 
//    WHERE c.Id = @Id
//";
//            #endregion

//            var result = CDbQueryExecutor.GetItem(new CChatInfoMapper(), sqlQuery);
//            return result;
//        }

//        [Obsolete("Use instance method instead of static")]
//        public static CChatInfo GetDialog(Guid userId, Guid participantId)
//        {
//            #region SQL Query
//            var sqlQuery = @"
//SELECT 
//    [Id] = c.Id, 
//    [Title] = c.Title, 
//    [OwnerId] = c.OwnerId, 
//    [IsPersonal] = c.IsPersonal,
//    [Type] = c.Type
//FROM (
//    SELECT DISTINCT 
//        p.ChatId
//    FROM chatsParticipants p 

//    INNER JOIN chatsParticipants p2 

//    ON p.ChatId = p2.ChatId
//        WHERE p.UserId = @UserId
//        GROUP BY p.ChatId
//        HAVING COUNT(p.ChatId) = 2
//    ) as res

//INNER JOIN chatsParticipants p 
//ON res.ChatId = p.ChatId

//INNER JOIN chats c
//ON p.ChatId = c.Id  
//    WHERE p.UserId = @ParticipantId
//";
//            #endregion

//            var result = CDbQueryExecutor.GetItemParametrized(new CChatInfoMapper(), sqlQuery,
//                SSqlParameterCreator.Create(
//                    "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
//                    ),
//                SSqlParameterCreator.Create(
//                    "@ParticipantId", participantId, System.Data.SqlDbType.UniqueIdentifier, false
//                    )
//                );
//            return result;
//        }

//        [Obsolete("Use instance method instead of static")]
//        public static CChatInfo CreateChat(CChatInfo chatInfo)
//        {
//            #region SQL Query
//            var sqlQuery = @"
//INSERT INTO 
//    chats
//VALUES (
//   @Id, @Title, @OwnerId, @IsPersonal, @Type
//)
//";
//            #endregion

//            var id = Guid.NewGuid();

//            CChatInfo output = null;

//            var result = CDbQueryExecutor.CreateItem(sqlQuery,
//                SSqlParameterCreator.Create(
//                    "@Id", id, System.Data.SqlDbType.UniqueIdentifier, false
//                ),
//                SSqlParameterCreator.Create(
//                    "@Title", chatInfo.Title, System.Data.SqlDbType.NVarChar, false
//                ),
//                SSqlParameterCreator.Create(
//                    "@OwnerId", chatInfo.OwnerId, System.Data.SqlDbType.UniqueIdentifier, false
//                    ),
//                SSqlParameterCreator.Create(
//                    "@IsPersonal", chatInfo.IsPersonal, System.Data.SqlDbType.Bit, false
//                    ),
//                SSqlParameterCreator.Create(
//                    "@Type", chatInfo.Type, System.Data.SqlDbType.TinyInt, false
//                )
//                );

//            output = new CChatInfo(id, chatInfo.Title, chatInfo.OwnerId, chatInfo.IsPersonal, chatInfo.Type);

//            return output;
//        }

//        #endregion


        public IList<CChatInfo> GetChatsByParticipantId(Guid userId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetChatsByParticipantId)}({userId})' is called");
            #region SQL Query

            var sqlQuery = @"
SELECT DISTINCT 
    [Id] = c.Id, 
    [Title] = c.Title, 
    [OwnerId] = c.OwnerId, 
    [IsPersonal] = c.IsPersonal,
    [Type] = c.Type
FROM chatsParticipants p 

INNER JOIN chats c 

ON p.ChatId = c.Id
    WHERE p.UserId = @UserId
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CChatInfoMapper(), sqlQuery,
                            SSqlParameterCreator.Create(
                                "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );
                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetChatsByParticipantId)}({userId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetChatsByParticipantId)}({userId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetChatsByParticipantId)}({userId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public Int32 GetUnreadMessagesCount(Guid userId, Guid chatId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetUnreadMessagesCount)}({userId}, {chatId})' is called");
            #region SQL Query
            var sqlQuery = @"
SELECT DISTINCT
    COUNT(*)
FROM messagesInChats
    WHERE ChatId = @ChatId
    AND ToUserId = @UserId
    AND IsRead = 0
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.ExecuteScalar<Int32>(sqlQuery,
                            SSqlParameterCreator.Create("@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false),
                            SSqlParameterCreator.Create("@ChatId", chatId, System.Data.SqlDbType.UniqueIdentifier, false)
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetUnreadMessagesCount)}({userId}, {chatId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetUnreadMessagesCount)}({userId}, {chatId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetUnreadMessagesCount)}({userId}, {chatId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        public IList<CChatInfo> GetChatsByOwnerId(Guid userId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetChatsByOwnerId)}({userId})' is called");
            #region SQL Query
            var sqlQuery = @"
SELECT 
    [Id] = Id, 
    [Title] = Title, 
    [OwnerId] = OwnerId, 
    [IsPersonal] = IsPersonal,
    [Type] = Type
FROM chats AS c 
    WHERE c.OwnerId = @OwnerId
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CChatInfoMapper(), sqlQuery);

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetChatsByOwnerId)}({userId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetChatsByOwnerId)}({userId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetChatsByOwnerId)}({userId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public CChatInfo GetChatById(Guid chatId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetChatById)}({chatId})' is called");
            #region SQL Query
            var sqlQuery = @"
SELECT 
    [Id] = Id, 
    [Title] = Title, 
    [OwnerId] = OwnerId, 
    [IsPersonal] = IsPersonal,
    [Type] = Type
FROM chats AS c 
    WHERE c.Id = @Id
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetItem(new CChatInfoMapper(), sqlQuery);

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetChatById)}({chatId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetChatById)}({chatId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetChatById)}({chatId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public CChatInfo GetDialog(Guid userId, Guid participantId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetDialog)}({userId}, {participantId})' is called");
            #region SQL Query
            var sqlQuery = @"
SELECT 
    [Id] = c.Id, 
    [Title] = c.Title, 
    [OwnerId] = c.OwnerId, 
    [IsPersonal] = c.IsPersonal,
    [Type] = c.Type
FROM (
    SELECT DISTINCT 
        p.ChatId
    FROM chatsParticipants p 

    INNER JOIN chatsParticipants p2 

    ON p.ChatId = p2.ChatId
        WHERE p.UserId = @UserId
        GROUP BY p.ChatId
        HAVING COUNT(p.ChatId) = 2
    ) as res

INNER JOIN chatsParticipants p 
ON res.ChatId = p.ChatId

INNER JOIN chats c
ON p.ChatId = c.Id  
    WHERE p.UserId = @ParticipantId
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetItem(new CChatInfoMapper(), sqlQuery,
                            SSqlParameterCreator.Create(
                                "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@ParticipantId", participantId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetDialog)}({userId}, {participantId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetDialog)}({userId}, {participantId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetDialog)}({userId}, {participantId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public CChatInfo CreateChat(CChatInfo chatInfo)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(CreateChat)}({chatInfo})' is called");
            #region SQL Query
            var sqlQuery = @"
INSERT INTO 
    chats
VALUES (
   @Id, @Title, @OwnerId, @IsPersonal, @Type
)
";
            #endregion


            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var id = Guid.NewGuid();
                        var result = executor.CreateItem(sqlQuery,
                            SSqlParameterCreator.Create(
                                "@Id", id, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@Title", chatInfo.Title, System.Data.SqlDbType.NVarChar, false
                            ),
                            SSqlParameterCreator.Create(
                                "@OwnerId", chatInfo.OwnerId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@IsPersonal", chatInfo.IsPersonal, System.Data.SqlDbType.Bit, false
                            ),
                            SSqlParameterCreator.Create(
                                "@Type", chatInfo.Type, System.Data.SqlDbType.TinyInt, false
                            )
                        );

                        executor.Commit();

                        return new CChatInfo(id, chatInfo.Title, chatInfo.OwnerId, chatInfo.IsPersonal, chatInfo.Type);
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(CreateChat)}({chatInfo}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(CreateChat)}({chatInfo}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(CreateChat)}({chatInfo}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }
    }
}


            //using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            //{
            //    using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
            //    {
            //        try
            //        {
            //            var result = executor.

            //            executor.Commit();
            //            return result;
            //        }
            //        catch (SqlException e)
            //        {
            //            s_log.LogError($@"{nameof(GetChatsByParticipantId)}({userId}): Error occured during SQL query execution", e);
            //            s_log.LogInfo($@"{nameof(GetChatsByParticipantId)}({userId}): Operation was rolled back because of error");
            //            Console.WriteLine($@"{nameof(GetChatsByParticipantId)}({userId}): Error occured during SQL query execution");
            //            Console.WriteLine("Operation was rolled back because of error");
            //            return null;
            //        }
            //    }
            //}