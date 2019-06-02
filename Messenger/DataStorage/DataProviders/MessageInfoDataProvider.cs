using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataStorage.Mappers;
using DataStorage.Other;
using Info;
using log4net;

namespace DataStorage.DataProviders
{
    public class CMessageInfoDataProvider : ICMessageInfoDataProvider
    {
        private readonly CDataStorageSettings _dbSettings;
        private static readonly ILog s_log = SLogger.GetLogger();

        public CMessageInfoDataProvider(CDataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }

        #region Static

        //        public static IList<CMessageInfo> GetAllMessagesFromChat(Guid userId, Guid chatId, Int32 limit, Int32 offset)
        //        {
        //            #region Sql

        //            var sql = @"
        //SELECT
        //    [Id] = res.Id,
        //    [DispatchDate] = res.DispatchDate,
        //    [MessageText] = res.MessageText,
        //    [Type] = res.Type,
        //    [ContentUri] = res.ContentUri,
        //    [FromUserId] = res.FromUserId,
        //    [IsRead] = res.IsRead
        //FROM 
        //(
        //    SELECT DISTINCT
        //        m.Id,
        //        m.DispatchDate,
        //        m.MessageText,
        //        m.Type,
        //        m.ContentUri,
        //        c.FromUserId,
        //        c.IsRead
        //    FROM messagesInChats c

        //    INNER JOIN messages m

        //    ON c.MessageId = m.Id
        //        WHERE c.ChatId = @ChatId
        //        AND c.ToUserId = @UserId
        //    ORDER BY m.DispatchDate DESC
        //    OFFSET @Offset ROWS
        //    FETCH NEXT @Limit ROWS ONLY
        //) res
        //ORDER BY res.DispatchDate
        //";

        //            #endregion

        //            return CDbQueryExecutor.GetDataParametrized(new CMessageInfoMapper(), sql,
        //                SSqlParameterCreator.Create("@ChatId", chatId, SqlDbType.UniqueIdentifier, false),
        //                SSqlParameterCreator.Create("@UserId", userId, SqlDbType.UniqueIdentifier, false),
        //                SSqlParameterCreator.Create("@Offset", offset, SqlDbType.Int, false),
        //                SSqlParameterCreator.Create("@Limit", limit, SqlDbType.Int, false)
        //            );
        //        }

        //        public static CMessageInfo CreateMessage(CMessageInfo message)
        //        {
        //            #region SQL

        //            var sql = @"
        //INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
        //    VALUES (
        //@Id, @DispatchDate, @MessageText, @Type, @ContentUri
        //);
        //";

        //            #endregion

        //            var outputId = Guid.NewGuid();
        //            var outputDispatchDate = DateTime.UtcNow;
        //            CMessageInfo output;

        //            try
        //            {
        //                var result = CDbQueryExecutor.CreateItemParametrized(sql,
        //                    SSqlParameterCreator.Create(
        //                        "@Id", outputId, SqlDbType.UniqueIdentifier, false, ParameterDirection.Input
        //                    ),
        //                    SSqlParameterCreator.Create(
        //                        "@DispatchDate", message.DispatchDate, SqlDbType.DateTimeOffset, false, ParameterDirection.Output
        //                    ),
        //                    SSqlParameterCreator.Create(
        //                        "@MessageText", message.MessageText, SqlDbType.NVarChar, true, ParameterDirection.Input, 2038
        //                    ),
        //                    SSqlParameterCreator.Create(
        //                        "@Type", message.Type, SqlDbType.TinyInt, false
        //                    ),
        //                    SSqlParameterCreator.Create(
        //                        "@ContentUri", message.ContentUri, SqlDbType.NVarChar, true, ParameterDirection.Input, 2038
        //                    )
        //                );

        //                output = new CMessageInfo(
        //                    outputId,
        //                    message.DispatchDate,
        //                    message.MessageText,
        //                    message.Type,
        //                    message.ContentUri,
        //                    message.FromUserId,
        //                    message.IsRead
        //                    );
        //            }
        //            catch (InvalidOperationException e)
        //            {
        //                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({message})", e);
        //                Console.WriteLine(e);
        //                throw;
        //            }

        //            return output;
        //        }

        //        public static CMessageInfo GetLastMessageFromChat(Guid chatId)
        //        {
        //            #region Sql

        //            var sql = @"
        //SELECT DISTINCT
        //    [Id] = m.Id,
        //    [DispatchDate] = m.DispatchDate,
        //    [MessageText] = m.MessageText,
        //    [Type] = m.Type,
        //    [ContentUri] = m.ContentUri,
        //    [FromUserId] = c.FromUserId,
        //    [IsRead] = c.IsRead
        //FROM messagesInChats c

        //INNER JOIN messages m

        //ON c.MessageId = m.Id
        //    WHERE c.ChatId = @ChatId
        //ORDER BY m.DispatchDate DESC
        //";

        //            #endregion

        //            return CDbQueryExecutor.GetItemParametrized(new CMessageInfoMapper(), sql,
        //                SSqlParameterCreator.Create("@ChatId", chatId, SqlDbType.UniqueIdentifier, false)
        //            );
        //        }

        //        public static IList<CMessageInfo> GetNewMessagesFromChat(Guid chatId, DateTimeOffset lastRequestDate, Int32 limit, Int32 offset)
        //        {
        //            #region Sql

        //            var sql = @"
        //SELECT
        //    [Id] = res.Id,
        //    [DispatchDate] = res.DispatchDate,
        //    [MessageText] = res.MessageText,
        //    [Type] = res.Type,
        //    [ContentUri] = res.ContentUri,
        //    [FromUserId] = res.FromUserId,
        //    [IsRead] = res.IsRead
        //FROM 
        //(
        //    SELECT DISTINCT
        //        m.Id,
        //        m.DispatchDate,
        //        m.MessageText,
        //        m.Type,
        //        m.ContentUri,
        //        c.FromUserId,
        //        c.IsRead
        //    FROM messagesInChats c

        //    INNER JOIN messages m

        //    ON c.MessageId = m.Id
        //        WHERE c.ChatId = @ChatId
        //        AND m.DispatchDate > @LastRequestDate
        //    ORDER BY m.DispatchDate DESC
        //    OFFSET @Offset ROWS
        //    FETCH NEXT @Limit ROWS ONLY
        //) res
        //ORDER BY res.DispatchDate
        //";

        //            #endregion

        //            return CDbQueryExecutor.GetDataParametrized(new CMessageInfoMapper(), sql,
        //                SSqlParameterCreator.Create("@ChatId", chatId, SqlDbType.UniqueIdentifier, false),
        //                SSqlParameterCreator.Create("@LastRequestDate", lastRequestDate, SqlDbType.DateTimeOffset, false),
        //                SSqlParameterCreator.Create("@Offset", offset, SqlDbType.Int, false),
        //                SSqlParameterCreator.Create("@Limit", limit, SqlDbType.Int, false)
        //            );
        //        } 
        #endregion

        //TODO Нужно ли использовать TOP(N)?
        public IList<CMessageInfo> GetAllMessagesFromChat(Guid userId, Guid chatId, Int32 limit, Int32 offset)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetAllMessagesFromChat)}({userId}, {chatId}, {limit}, {offset})' is called");
            //TODO Надо бы добавить информацию об авторе сообщения
            #region Sql

            var sql = @"
SELECT
    [Id] = res.Id,
    [DispatchDate] = res.DispatchDate,
    [MessageText] = res.MessageText,
    [Type] = res.Type,
    [ContentUri] = res.ContentUri,
    [FromUserId] = res.FromUserId,
    [IsRead] = res.IsRead,
    [Login] = res.Login,
    [USN] = res.USN
FROM 
(
    SELECT DISTINCT
        m.Id,
        m.DispatchDate,
        m.MessageText,
        m.Type,
        m.ContentUri,
        c.FromUserId,
        c.IsRead,
        u.Login,
        m.USN
    FROM messagesInChats c

    INNER JOIN messages m
    ON c.MessageId = m.Id
    
    INNER JOIN users u
    ON u.Id = c.FromUserId
        WHERE c.ChatId = @ChatId
        AND c.ToUserId = @UserId
    ORDER BY m.DispatchDate DESC
    OFFSET @Offset ROWS
    FETCH NEXT @Limit ROWS ONLY
) res
ORDER BY res.DispatchDate
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CMessageInfoMapper(), sql,
                            SSqlParameterCreator.Create("@ChatId", chatId, SqlDbType.UniqueIdentifier, false),
                            SSqlParameterCreator.Create("@UserId", userId, SqlDbType.UniqueIdentifier, false),
                            SSqlParameterCreator.Create("@Offset", offset, SqlDbType.Int, false),
                            SSqlParameterCreator.Create("@Limit", limit, SqlDbType.Int, false)
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetAllMessagesFromChat)}({userId}, {chatId}, {limit}, {offset}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetAllMessagesFromChat)}({userId}, {chatId}, {limit}, {offset}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetAllMessagesFromChat)}({userId}, {chatId}, {limit}, {offset}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        //TODO Либо генерировать у пользователя GUID и передавать его, либо генерировать его здесь и возвращать как результат операции
        //TODO Соответственно, MessageInChat либо генерируется по запросам клиента, либо "вшить" генерацию внутрь контроллера 
        //TODO (когда посылаем и создаем сообщениеб тогда же и создаем MsgInChat)
        public CMessageInfo CreateMessage(CMessageInfo message)
        {            
            s_log.LogInfo($@"Data provider's method '{nameof(CreateMessage)}({message})' is called");
            
            #region SQL

            var sql = @"
INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
    VALUES (
@Id, @DispatchDate, @MessageText, @Type, @ContentUri
);
SELECT USN 
FROM messages 
    WHERE Id = @Id;
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var outputId = Guid.NewGuid();

                        var result = executor.ExecuteScalar<Int64>(sql,
                            SSqlParameterCreator.Create(
                                "@Id", outputId, SqlDbType.UniqueIdentifier, false, ParameterDirection.Input
                            ),
                            SSqlParameterCreator.Create(
                                "@DispatchDate", message.DispatchDate, SqlDbType.DateTimeOffset, false, ParameterDirection.Input
                            ),
                            SSqlParameterCreator.Create(
                                "@MessageText", message.MessageText, SqlDbType.NVarChar, true, ParameterDirection.Input, 2038
                            ),
                            SSqlParameterCreator.Create(
                                "@Type", message.Type, SqlDbType.TinyInt, false
                            ),
                            SSqlParameterCreator.Create(
                                "@ContentUri", message.ContentUri, SqlDbType.NVarChar, true, ParameterDirection.Input, 2038
                            )
                        );

                        executor.Commit();

                        return new CMessageInfo(outputId, message.DispatchDate, message.MessageText, message.Type,
                            message.ContentUri, message.FromUserId, message.IsRead, message.Login, result);
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(CreateMessage)}({message}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(CreateMessage)}({message}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(CreateMessage)}({message}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public CMessageInfo GetLastMessageFromChat(Guid chatId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetLastMessageFromChat)}({chatId})' is called");
            
            #region Sql

            var sql = @"
SELECT DISTINCT
    [Id] = m.Id,
    [DispatchDate] = m.DispatchDate,
    [MessageText] = m.MessageText,
    [Type] = m.Type,
    [ContentUri] = m.ContentUri,
    [FromUserId] = c.FromUserId,
    [IsRead] = c.IsRead,
    [Login] = u.Login,
    [USN] = m.USN
FROM messagesInChats c

INNER JOIN messages m
ON c.MessageId = m.Id

INNER JOIN users u
ON c.FromUserId = u.Id
    WHERE c.ChatId = @ChatId
ORDER BY m.DispatchDate DESC
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetItem(new CMessageInfoMapper(), sql,
                            SSqlParameterCreator.Create("@ChatId", chatId, SqlDbType.UniqueIdentifier, false)
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetLastMessageFromChat)}({chatId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetLastMessageFromChat)}({chatId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetLastMessageFromChat)}({chatId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public IList<CMessageInfo> GetNewMessagesFromChat(Guid userId, Guid chatId, DateTimeOffset lastRequestDate, Int32 limit, Int32 offset, Int64 usn)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetNewMessagesFromChat)}({chatId}, {lastRequestDate}, {limit}, {offset})' is called");
            //TODO Надо добавить информацию об авторе сообщения
            #region Sql

            var sql = @"
SELECT
    [Id] = res.Id,
    [DispatchDate] = res.DispatchDate,
    [MessageText] = res.MessageText,
    [Type] = res.Type,
    [ContentUri] = res.ContentUri,
    [FromUserId] = res.FromUserId,
    [IsRead] = res.IsRead,
    [Login] = res.Login,
    [USN] = res.USN
FROM 
(
    SELECT DISTINCT
        m.Id,
        m.DispatchDate,
        m.MessageText,
        m.Type,
        m.ContentUri,
        c.FromUserId,
        c.IsRead,
        u.Login,
        m.USN
    FROM messagesInChats c

    INNER JOIN messages m
    ON c.MessageId = m.Id

    INNER JOIN users u
    ON c.FromUserId = u.Id
        WHERE c.ChatId = @ChatId
        AND c.ToUserId = @UserId 
        AND c.FromUserId != @UserId
        AND m.DispatchDate > @LastRequestDate
        AND m.USN > @USN
    ORDER BY m.DispatchDate DESC
    OFFSET @Offset ROWS
    FETCH NEXT @Limit ROWS ONLY
) res
ORDER BY res.DispatchDate
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CMessageInfoMapper(), sql,
                            SSqlParameterCreator.Create("@UserId", userId, SqlDbType.UniqueIdentifier, false),
                            SSqlParameterCreator.Create("@ChatId", chatId, SqlDbType.UniqueIdentifier, false),
                            SSqlParameterCreator.Create("@LastRequestDate", lastRequestDate, SqlDbType.DateTimeOffset, false),
                            SSqlParameterCreator.Create("@Offset", offset, SqlDbType.Int, false),
                            SSqlParameterCreator.Create("@Limit", limit, SqlDbType.Int, false),
                            SSqlParameterCreator.Create("@USN", usn, SqlDbType.BigInt, false)
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetNewMessagesFromChat)}({chatId}, {lastRequestDate}, {limit}, {offset}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetNewMessagesFromChat)}({chatId}, {lastRequestDate}, {limit}, {offset}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetNewMessagesFromChat)}({chatId}, {lastRequestDate}, {limit}, {offset}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }
    }
}