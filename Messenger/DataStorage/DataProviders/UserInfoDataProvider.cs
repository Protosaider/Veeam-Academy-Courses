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
    public class CUserInfoDataProvider : ICUserInfoDataProvider
    {
        private readonly CDataStorageSettings _dbSettings;
        private static readonly ILog s_log = SLogger.GetLogger();

        public CUserInfoDataProvider(CDataStorageSettings dbSettings)
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));
        }

            #region Static
        //        public static CUserInfo GetUserByAuthData(String login, String password)
        //        {
        //            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({login}, {password}) is called");

        //            #region SQL Query
        //            var sql = @"
        //SELECT 
        //    [Id] = u.Id,
        //    [Login] = u.Login,
        //    [Password] = u.Password,
        //    [LastActiveDate] = u.LastActiveDate,
        //    [ActivityStatus] = u.ActivityStatus,
        //    [Avatar] = u.Avatar
        //FROM users AS u 
        //    WHERE u.Login = @Login
        //    AND u.Password = @Password
        //";
        //            #endregion

        //            var result = CDbQueryExecutor.GetItemParametrized(new CUserInfoMapper(), sql,
        //                SSqlParameterCreator.Create(
        //                    "@Login", login, System.Data.SqlDbType.NVarChar, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@Password", password, System.Data.SqlDbType.NVarChar, false
        //                    )
        //                );
        //            return result;
        //        }

        //        public static Int32 UpdateUserLastActiveDate(Guid userId, DateTimeOffset lastActiveDate)
        //        {
        //            var sql = @"
        //UPDATE users

        //SET LastActiveDate = (CASE 
        //        WHEN LastActiveDate < @LastActiveDate 
        //        THEN @LastActiveDate 
        //        ELSE LastActiveDate 
        //    END) 
        //    WHERE Id = @UserId
        //";


        //            return CDbQueryExecutor.UpdateItemParametrized(sql,
        //                SSqlParameterCreator.Create(
        //                    "@Id", userId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@LastActiveDate", lastActiveDate, SqlDbType.DateTimeOffset, true
        //                    )
        //                );
        //        }

        //        public static CUserInfo GetUserData(Guid id)
        //        {
        //            #region Sql

        //            var sql = @"
        //SELECT 
        //    [Id] = u.Id,
        //    [Login] = u.Login,
        //    [Password] = u.Password,
        //    [LastActiveDate] = u.LastActiveDate,
        //    [ActivityStatus] = u.ActivityStatus,
        //    [Avatar] = u.Avatar
        //FROM users AS u 
        //    WHERE u.Id = @Id
        //";

        //            #endregion

        //            return CDbQueryExecutor.GetItemParametrized(new CUserInfoMapper(), sql,
        //                SSqlParameterCreator.Create(
        //                    "@Id", id, System.Data.SqlDbType.UniqueIdentifier, false
        //                    )
        //                );
        //        }

        //        public static Int32 UpdateUserStatus(Guid userId, Int32 currentStatus)
        //        {
        //            #region Sql

        //            var sql = @"
        //UPDATE users
        //SET ActivityStatus = @CurrentStatus
        //    WHERE Id = @UserId
        //";

        //            #endregion

        //            return CDbQueryExecutor.UpdateItemParametrized(sql,
        //                SSqlParameterCreator.Create(
        //                    "@Id", userId, System.Data.SqlDbType.UniqueIdentifier, false
        //                    ),
        //                SSqlParameterCreator.Create(
        //                    "@CurrentStatus", (Byte)currentStatus, SqlDbType.TinyInt, false
        //                    )
        //                );
        //        }

        //        //TODO Куда этот метод убрать? Или оставить здесь?
        //        public static IList<CUserInfo> GetAllChatParticipantsByChatId(Guid chatId)
        //        {
        //            #region Sql

        //            var sql = @"
        //SELECT 
        //    [Id] = u.Id,
        //    [Login] = u.Login,
        //    [Password] = u.Password,
        //    [LastActiveDate] = u.LastActiveDate,
        //    [ActivityStatus] = u.ActivityStatus,
        //    [Avatar] = u.Avatar
        //FROM chatsParticipants c

        //INNER JOIN users u

        //ON c.UserId = u.Id
        //    WHERE c.ChatId = @ChatId
        //";

        //            #endregion

        //            return CDbQueryExecutor.GetDataParametrized(new CUserInfoMapper(), sql,
        //                SSqlParameterCreator.Create(
        //                    "@ChatId", chatId, SqlDbType.UniqueIdentifier, false
        //                    )
        //            );
        //        }

        //        public static Guid CreateUser(CUserInfo user)
        //        {
        //            #region SQL Query
        //            var sql = @"
        //INSERT INTO users (Id, Login, Password, LastActiveDate, ActivityStatus, Avatar)
        //    VALUES (
        //@Id, @Login, @Password, @LastActiveDate, @ActivityStatus, @Avatar
        //)
        //";
        //            #endregion

        //            var newId = Guid.NewGuid();

        //            try
        //            {
        //                var result = CDbQueryExecutor.CreateItemParametrized(sql,
        //                    SSqlParameterCreator.Create(
        //                        "@Id", newId, SqlDbType.UniqueIdentifier, false
        //                        ),
        //                    SSqlParameterCreator.Create(
        //                        "@Login", user.Login, System.Data.SqlDbType.NVarChar, false
        //                        ),
        //                    SSqlParameterCreator.Create(
        //                        "@Password", user.Password, System.Data.SqlDbType.NVarChar, false
        //                        ),
        //                    SSqlParameterCreator.Create(
        //                        "@LastActiveDate", user.LastActiveDate, System.Data.SqlDbType.DateTimeOffset, true
        //                        ),
        //                    SSqlParameterCreator.Create(
        //                        "@ActivityStatus", user.ActivityStatus, System.Data.SqlDbType.TinyInt, false
        //                        ),
        //                    SSqlParameterCreator.Create(
        //                        "@Avatar", user.Avatar, System.Data.SqlDbType.NVarChar, true
        //                        )
        //                    );
        //            }
        //            catch (InvalidOperationException e)
        //            {
        //                Console.WriteLine(e);
        //                throw;
        //            }

        //            return newId;
        //        }

        //        public static Int32 DeleteUser(Guid id)
        //        {
        //            #region SQL Query
        //            var sql = @"
        //DELETE FROM users 
        //    WHERE users.Id = @Id
        //";
        //            #endregion

        //            return CDbQueryExecutor.DeleteItemParametrized(sql,
        //                SSqlParameterCreator.Create(
        //                    "@Id", id, System.Data.SqlDbType.UniqueIdentifier, false
        //                    )
        //                );
        //        }

        //        public static IList<CUserInfo> GetAllNotOfflineUsers()
        //        {
        //            #region Sql

        //            var sql = @"
        //SELECT 
        //    [Id] = Id,
        //    [Login] = Login,
        //    [Password] = Password,
        //    [LastActiveDate] = LastActiveDate,
        //    [ActivityStatus] = ActivityStatus,
        //    [Avatar] = Avatar
        //FROM users
        //    WHERE ActivityStatus != 0
        //";

        //            #endregion

        //            return CDbQueryExecutor.GetDataParametrized(new CUserInfoMapper(), sql);
        //        }

        //        public static IList<CUserInfo> SearchContacts(Guid ownerId, String q)
        //        {
        //            #region SQL Query
        //            //            var sql = @"
        //            //SELECT DISTINCT
        //            //    [Id] = c.Id,
        //            //    [OwnerId] = c.OwnerId,
        //            //    [UserId] = c.UserId,
        //            //    [IsBlocked] = c.IsBlocked
        //            //FROM contactsLists c

        //            //INNER JOIN users u
        //            //ON c.UserId = u.Id
        //            //    WHERE c.OwnerId != @OwnerId
        //            //    AND CONTAINS (u.Login, @SearchQuery)
        //            //";

        //            var sql = @"
        //SELECT
        //    [Id] = u.Id,
        //    [Login] = u.Login,
        //    [Password] = u.Password,
        //    [LastActiveDate] = u.LastActiveDate,
        //    [ActivityStatus] = u.ActivityStatus,
        //    [Avatar] = u.Avatar
        //FROM users u
        //    WHERE NOT EXISTS (
        //        SELECT * 
        //        FROM contactsLists c 
        //            WHERE c.OwnerId = @OwnerId 
        //            AND u.Id = c.UserId
        //            OR u.Id = @OwnerId 
        //    )
        //    AND u.Login LIKE @SearchQuery
        //";
        //            #endregion

        //            var result = CDbQueryExecutor.GetDataParametrized(new CUserInfoMapper(), sql,
        //                SSqlParameterCreator.Create(
        //                    "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
        //                ),
        //                SSqlParameterCreator.Create(
        //                    "@SearchQuery", "%" + q + "%", System.Data.SqlDbType.NVarChar, false
        //                )
        //                );

        //            return result;
        //        } 
                #endregion

        public CUserInfo GetUserByAuthData(String login, String password)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetUserByAuthData)}({login}, {password})' is called");

            #region SQL Query
            var sql = @"
SELECT 
    [Id] = u.Id,
    [Login] = u.Login,
    [Password] = u.Password,
    [LastActiveDate] = u.LastActiveDate,
    [ActivityStatus] = u.ActivityStatus,
    [Avatar] = u.Avatar
FROM users AS u 
    WHERE u.Login = @Login
    AND u.Password = @Password
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetItem(new CUserInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@Login", login, System.Data.SqlDbType.NVarChar, false
                            ),
                            SSqlParameterCreator.Create(
                                "@Password", password, System.Data.SqlDbType.NVarChar, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetUserByAuthData)}({login}, {password}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetUserByAuthData)}({login}, {password}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetUserByAuthData)}({login}, {password}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public Int32 UpdateUserLastActiveDate(Guid userId, DateTimeOffset lastActiveDate)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(UpdateUserLastActiveDate)}({userId}, {lastActiveDate})' is called");

            #region SQL

            var sql = @"
UPDATE users

SET LastActiveDate = (CASE 
        WHEN LastActiveDate < @LastActiveDate 
        THEN @LastActiveDate 
        ELSE LastActiveDate 
    END) 
    WHERE Id = @UserId
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.UpdateItem(sql,
                            SSqlParameterCreator.Create(
                                "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@LastActiveDate", lastActiveDate, SqlDbType.DateTimeOffset, true
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(UpdateUserLastActiveDate)}({userId}, {lastActiveDate}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(UpdateUserLastActiveDate)}({userId}, {lastActiveDate}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(UpdateUserLastActiveDate)}({userId}, {lastActiveDate}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        public CUserInfo GetUserData(Guid userId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetUserData)}({userId})' is called");

            #region Sql

            var sql = @"
SELECT 
    [Id] = u.Id,
    [Login] = u.Login,
    [Password] = u.Password,
    [LastActiveDate] = u.LastActiveDate,
    [ActivityStatus] = u.ActivityStatus,
    [Avatar] = u.Avatar
FROM users AS u 
    WHERE u.Id = @Id
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetItem(new CUserInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@Id", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetUserData)}({userId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetUserData)}({userId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetUserData)}({userId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public Int32 UpdateUserStatus(Guid userId, Int32 currentStatus)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(UpdateUserStatus)}({userId}, {currentStatus})' is called");

            #region Sql

            var sql = @"
UPDATE users
SET ActivityStatus = @CurrentStatus
    WHERE Id = @UserId
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.UpdateItem(sql,
                            SSqlParameterCreator.Create(
                                "@UserId", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@CurrentStatus", (Byte)currentStatus, SqlDbType.TinyInt, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(UpdateUserStatus)}({userId}, {currentStatus}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(UpdateUserStatus)}({userId}, {currentStatus}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(UpdateUserStatus)}({userId}, {currentStatus}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        //TODO Куда этот метод убрать? Или оставить здесь?
        public IList<CUserInfo> GetAllChatParticipantsByChatId(Guid chatId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetAllChatParticipantsByChatId)}({chatId})' is called");

            #region Sql

            var sql = @"
SELECT 
    [Id] = u.Id,
    [Login] = u.Login,
    [Password] = u.Password,
    [LastActiveDate] = u.LastActiveDate,
    [ActivityStatus] = u.ActivityStatus,
    [Avatar] = u.Avatar
FROM chatsParticipants c

INNER JOIN users u

ON c.UserId = u.Id
    WHERE c.ChatId = @ChatId
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CUserInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@ChatId", chatId, SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetAllChatParticipantsByChatId)}({chatId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetAllChatParticipantsByChatId)}({chatId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetAllChatParticipantsByChatId)}({chatId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public Guid CreateUser(CUserInfo user)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(CreateUser)}({user})' is called");

            #region SQL Query
            var sql = @"
INSERT INTO users (Id, Login, Password, LastActiveDate, ActivityStatus, Avatar)
    VALUES (
@Id, @Login, @Password, @LastActiveDate, @ActivityStatus, @Avatar
)
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var newId = Guid.NewGuid();

                        var result = executor.CreateItem(sql,
                            SSqlParameterCreator.Create(
                                "@Id", newId, SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@Login", user.Login, System.Data.SqlDbType.NVarChar, false
                            ),
                            SSqlParameterCreator.Create(
                                "@Password", user.Password, System.Data.SqlDbType.NVarChar, false
                            ),
                            SSqlParameterCreator.Create(
                                "@LastActiveDate", user.LastActiveDate, System.Data.SqlDbType.DateTimeOffset, true
                            ),
                            SSqlParameterCreator.Create(
                                "@ActivityStatus", user.ActivityStatus, System.Data.SqlDbType.TinyInt, false
                            ),
                            SSqlParameterCreator.Create(
                                "@Avatar", user.Avatar, System.Data.SqlDbType.NVarChar, true
                            )
                        );

                        executor.Commit();
                        return newId;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(CreateUser)}({user}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(CreateUser)}({user}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(CreateUser)}({user}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return default(Guid);
                    }
                }
            }
        }

        public Int32 DeleteUser(Guid userId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(DeleteUser)}({userId})' is called");

            #region SQL Query
            var sql = @"
DELETE FROM users 
    WHERE users.Id = @Id
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.DeleteItem(sql,
                            SSqlParameterCreator.Create(
                                "@Id", userId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(DeleteUser)}({userId}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(DeleteUser)}({userId}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(DeleteUser)}({userId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return 0;
                    }
                }
            }
        }

        public IList<CUserInfo> GetAllNotOfflineUsers()
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetAllNotOfflineUsers)}()' is called");

            #region Sql

            var sql = @"
SELECT 
    [Id] = Id,
    [Login] = Login,
    [Password] = Password,
    [LastActiveDate] = LastActiveDate,
    [ActivityStatus] = ActivityStatus,
    [Avatar] = Avatar
FROM users
    WHERE ActivityStatus != 0
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CUserInfoMapper(), sql);

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetAllNotOfflineUsers)}(): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(GetAllNotOfflineUsers)}(): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(GetAllNotOfflineUsers)}(): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public IList<CUserInfo> SearchContacts(Guid ownerId, String q)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(SearchContacts)}({ownerId}, {q})' is called");

            #region SQL Query
            //            var sql = @"
            //SELECT DISTINCT
            //    [Id] = c.Id,
            //    [OwnerId] = c.OwnerId,
            //    [UserId] = c.UserId,
            //    [IsBlocked] = c.IsBlocked
            //FROM contactsLists c

            //INNER JOIN users u
            //ON c.UserId = u.Id
            //    WHERE c.OwnerId != @OwnerId
            //    AND CONTAINS (u.Login, @SearchQuery)
            //";

            var sql = @"
SELECT
    [Id] = u.Id,
    [Login] = u.Login,
    [Password] = u.Password,
    [LastActiveDate] = u.LastActiveDate,
    [ActivityStatus] = u.ActivityStatus,
    [Avatar] = u.Avatar
FROM users u
    WHERE NOT EXISTS (
        SELECT * 
        FROM contactsLists c 
            WHERE c.OwnerId = @OwnerId 
            AND u.Id = c.UserId
            OR u.Id = @OwnerId 
    )
    AND u.Login LIKE @SearchQuery
";
            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CUserInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
                            ),
                            SSqlParameterCreator.Create(
                                "@SearchQuery", "%" + q + "%", System.Data.SqlDbType.NVarChar, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(SearchContacts)}({ownerId}, {q}): Error occured during SQL query execution", e);
                        s_log.LogInfo($@"{nameof(SearchContacts)}({ownerId}, {q}): Operation was rolled back because of error");
                        Console.WriteLine($@"{nameof(SearchContacts)}({ownerId}, {q}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }

        public IList<CUserInfo> GetContactsLastActiveDate(Guid ownerId)
        {
            s_log.LogInfo($@"Data provider's method '{nameof(GetContactsLastActiveDate)}({ownerId})' is called");

            #region SQL Query

            var sql = @"
SELECT 
    [Id] = u.Id,
    [Login] = u.Login,
    [Password] = u.Password,
    [LastActiveDate] = u.LastActiveDate,
    [ActivityStatus] = u.ActivityStatus,
    [Avatar] = u.Avatar
FROM contactsLists c 

INNER JOIN users u

ON c.UserId = u.Id 
    WHERE c.OwnerId = @OwnerId
";

            #endregion

            using (IDbConnection connection = new SqlConnection(_dbSettings.DbConnectionString))
            {
                using (CDbTransactionQueryExecutor executor = new CDbTransactionQueryExecutor(connection))
                {
                    try
                    {
                        var result = executor.GetData(new CUserInfoMapper(), sql,
                            SSqlParameterCreator.Create(
                                "@OwnerId", ownerId, System.Data.SqlDbType.UniqueIdentifier, false
                            )
                        );

                        executor.Commit();
                        return result;
                    }
                    catch (SqlException e)
                    {
                        s_log.LogError($@"{nameof(GetContactsLastActiveDate)}({ownerId}): Error occured during SQL query execution",
                            e);
                        s_log.LogInfo(
                            $@"{nameof(GetContactsLastActiveDate)}({ownerId}): Operation was rolled back because of error");
                        Console.WriteLine(
                            $@"{nameof(GetContactsLastActiveDate)}({ownerId}): Error occured during SQL query execution");
                        Console.WriteLine("Operation was rolled back because of error");
                        return null;
                    }
                }
            }
        }
    }
}
