﻿-- =========================
-- Drop Database Template
-- =========================
USE master
GO

-- Drop the database if it already exists
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'messenger_db'
)
DROP DATABASE [messenger_db]
GO