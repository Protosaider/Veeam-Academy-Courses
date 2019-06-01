-- =========================================
-- Alter table 
-- =========================================
USE messenger_db
GO

ALTER TABLE users ADD Password VARCHAR(128) NULL
GO

UPDATE users SET Password = N'Default' WHERE Password IS NULL
GO

ALTER TABLE users ALTER COLUMN Password VARCHAR(128) NOT NULL
GO

UPDATE users SET Password = Login
GO

-- =========================================

ALTER TABLE chats ADD Type INT NULL
GO

UPDATE chats SET Type = 0 WHERE Type IS NULL
GO

ALTER TABLE chats ALTER COLUMN Type INT NOT NULL
GO

UPDATE chats SET Type = 1 WHERE Id = N'A08C057B-0E40-E911-9845-78843CFE6B41'
GO

-- =========================================

