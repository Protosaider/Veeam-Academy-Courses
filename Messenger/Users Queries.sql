--get user id
SELECT 
    [Id] = Id
FROM users AS u 
    WHERE u.Login = N'alpha'
    --WHERE u.Login = @Login

--get all user data by id
SELECT
    [Id] = Id,
    [Login] = Login,
    [LastActiveDate] = LastActiveDate,
    [ActivityStatus] = ActivityStatus,
    [Avatar] = Avatar
FROM users AS u
    WHERE u.Id = N'DCF534D2-0A40-E911-9845-78843CFE6B4B'
    --WHERE u.Id = @Id

--create user
INSERT INTO users (Id, Login, LastActiveDate, ActivityStatus, Avatar)
    VALUES (
DEFAULT, N'echo', SYSDATETIME(), 0, N'URI'
--DEFAULT, @Login, SYSDATETIME(), @ActivityStatus, @Avatar
)

-- Update user activityStatus
UPDATE users
    SET
ActivityStatus = 0
--ActivityStatus = @ActivityStatus
    WHERE Id = N'DCF534D2-0A40-E911-9845-78843CFE6B4B'
    --WHERE Id = @Id

-- Update User LastActiveDate
UPDATE users
    SET
LastActiveDate = SYSDATETIME()
--LastActiveDate = @LastActiveDate
    WHERE Id = N'DCF534D2-0A40-E911-9845-78843CFE6B4B'
    --WHERE Id = @Id

