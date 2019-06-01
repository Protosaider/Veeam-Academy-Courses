using System;
using System.Data;
using System.Data.SqlClient;
using Info;
using log4net;
using Other;

namespace DataStorage.DataProviders
{
    public class CChatsParticipantInfoDataProvider : ICChatsParticipantInfoDataProvider
    {
        private readonly CDataStorageSettings _dbSettings;
        private static readonly ILog s_log = SLogger.GetLogger();

        public CChatsParticipantInfoDataProvider(CDataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }

        //        #region Static
        //        public static Int32 CreateChatParticipant(CChatsParticipantInfo chatParticipantInfo)
        //        {
        //            #region Sql

        //            var sql = @"
        //INSERT INTO chatsParticipants (Id, ChatId, UserId)
        //    VALUES (
        //@Id, @ChatId, @UserId
        //)
        //";

        //            #endregion

        //            var id = Guid.NewGuid();

        //            return CDbQueryExecutor.CreateItem(sql,
        //                SSqlParameterCreator.Create(
        //                    "@Id", id, SqlDbType.UniqueIdentifier, false
        //                ),
        //                SSqlParameterCreator.Create(
        //                    "@ChatId", chatParticipantInfo.ChatId, SqlDbType.UniqueIdentifier, false
        //                ),
        //                SSqlParameterCreator.Create(
        //                    "@UserId", chatParticipantInfo.UserId, SqlDbType.UniqueIdentifier, false
        //                )
        //            );
        //        }
        //        #endregion

        public Int32 CreateChatParticipant(CChatsParticipantInfo chatParticipantInfo)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(CreateChatParticipant)}({chatParticipantInfo})' is called");

            #region Sql

            var sql = @"
INSERT INTO chatsParticipants (Id, ChatId, UserId)
    VALUES (
@Id, @ChatId, @UserId
)
";

            #endregion

            var id = Guid.NewGuid();

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.CreateItem(sql,
                            SSqlParameterCreator.Create(
                                "@Id", id, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@ChatId", chatParticipantInfo.ChatId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@UserId", chatParticipantInfo.UserId, SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(CreateChatParticipant)}({chatParticipantInfo}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(CreateChatParticipant)}({chatParticipantInfo}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(CreateChatParticipant)}({chatParticipantInfo}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }
    }
}