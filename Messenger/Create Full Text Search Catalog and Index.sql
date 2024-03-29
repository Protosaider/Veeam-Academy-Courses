-- Full-text search

USE messenger_db
GO

SELECT 
	CASE FULLTEXTSERVICEPROPERTY('IsFullTextInstalled')
		WHEN 1 THEN 'Full-Text installed.' 
		ELSE 'Full-Text is NOT installed.' 
	END
GO

SELECT is_fulltext_enabled
FROM sys.databases
    WHERE database_id = DB_ID()
GO

CREATE FULLTEXT CATALOG MessangerCatalog
    WITH ACCENT_SENSITIVITY = ON
    AS DEFAULT
    AUTHORIZATION dbo
GO

CREATE FULLTEXT INDEX ON users(Login)
    KEY INDEX PK_users_Id ON (MessangerCatalog) 
    WITH (CHANGE_TRACKING AUTO)
GO

SELECT DISTINCT
    [Id] = c.Id,
    [OwnerId] = c.OwnerId,
    [UserId] = c.UserId,
    [IsBlocked] = c.IsBlocked
FROM contactsLists c

INNER JOIN users u
ON c.UserId = u.Id
    WHERE c.OwnerId != N'DCF534D2-0A40-E911-9845-78843CFE6B4B'
    AND CONTAINS (u.Login, N'de')
GO

SELECT DISTINCT
    [Id] = c.Id,
    [OwnerId] = c.OwnerId,
    [UserId] = c.UserId,
    [IsBlocked] = c.IsBlocked
FROM contactsLists c

INNER JOIN users u
ON c.UserId = u.Id
    WHERE c.OwnerId != N'DCF534D2-0A40-E911-9845-78843CFE6B4B'
    AND u.Login LIKE ('%de%')
GO