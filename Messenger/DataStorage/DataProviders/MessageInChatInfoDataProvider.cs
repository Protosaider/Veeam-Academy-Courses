using System;
using System.Data;
using System.Data.SqlClient;
using DataStorage.Other;
using Info;
using log4net;

namespace DataStorage.DataProviders
{
    public class CMessageInChatInfoDataProvider : ICMessageInChatInfoDataProvider
    {
        private readonly CDataStorageSettings _dbSettings;
        private static readonly ILog s_log = SLogger.GetLogger();

        public CMessageInChatInfoDataProvider(CDataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }

        //        #region Static
        //        public static Int32 CreateMessageInChat(CMessageInChatInfo messageInChat)
        //        {
        //            #region Sql

        //            var sql = @"
        //INSERT INTO messagesInChats (Id, MessageId, ChatId, FromUserId, ToUserId, IsRead)
        //    VALUES (
        //DEFAULT, @MessageId, @ChatId, @FromUserId, @ToUserId, @IsRead
        //)
        //";

        //            #endregion

        //            return CDbQueryExecutor.CreateItemParametrized(sql,
        //                SSqlParameterCreator.Create(
        //                    "@MessageId", messageInChat.MessageId, SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@ChatId", messageInChat.ChatId, SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@FromUserId", messageInChat.FromUserId, SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@ToUserId", messageInChat.ToUserId, SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@IsRead", messageInChat.IsRead, SqlDbType.Bit, false
        //                    )
        //            );
        //        }

        //        public static Int32 UpdateReadMessageInChat(CMessageInChatInfo messageInChat)
        //        {
        //            #region Sql

        //            var sql = @"
        //UPDATE messagesInChats
        //SET IsRead = @IsRead
        //    WHERE ToUserId = @ToUserId
        //    AND MessageId = @MessageId
        //";

        //            #endregion

        //            return CDbQueryExecutor.CreateItemParametrized(sql,
        //                SSqlParameterCreator.Create(
        //                    "@MessageId", messageInChat.MessageId, SqlDbType.UniqueIdentifier, false
        //                ),
        //                SSqlParameterCreator.Create(
        //                    "@ToUserId", messageInChat.ToUserId, SqlDbType.UniqueIdentifier, false
        //                ),
        //                SSqlParameterCreator.Create(
        //                    "@IsRead", messageInChat.IsRead, SqlDbType.Bit, false
        //                )
        //            );
        //        } 
        //        #endregion

        public Int32 CreateMessageInChat(CMessageInChatInfo messageInChat)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(CreateMessageInChat)}({messageInChat})' is called");

            #region Sql

            var sql = @"
INSERT INTO messagesInChats (Id, MessageId, ChatId, FromUserId, ToUserId, IsRead)
    VALUES (
DEFAULT, @MessageId, @ChatId, @FromUserId, @ToUserId, @IsRead
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
                                "@MessageId", messageInChat.MessageId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@ChatId", messageInChat.ChatId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@FromUserId", messageInChat.FromUserId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@ToUserId", messageInChat.ToUserId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@IsRead", messageInChat.IsRead, SqlDbType.Bit, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(CreateMessageInChat)}({messageInChat}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(CreateMessageInChat)}({messageInChat}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(CreateMessageInChat)}({messageInChat}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        public Int32 UpdateReadMessageInChat(CMessageInChatInfo messageInChat)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(UpdateReadMessageInChat)}({messageInChat})' is called");

            #region Sql

            var sql = @"
UPDATE messagesInChats
SET IsRead = @IsRead
    WHERE ToUserId = @ToUserId
    AND MessageId = @MessageId
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
                                "@MessageId", messageInChat.MessageId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@ToUserId", messageInChat.ToUserId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@IsRead", messageInChat.IsRead, SqlDbType.Bit, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(UpdateReadMessageInChat)}({messageInChat}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(UpdateReadMessageInChat)}({messageInChat}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(UpdateReadMessageInChat)}({messageInChat}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }
    }
}