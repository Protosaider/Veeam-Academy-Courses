-- =========================================
-- Create table template
-- =========================================
USE messenger_db
GO

CREATE TABLE users(
    Id UNIQUEIDENTIFIER CONSTRAINT PK_users_Id PRIMARY KEY DEFAULT NEWID(),
	Login NVARCHAR(32) CONSTRAINT UQ_users_Login UNIQUE NOT NULL,
    LastActiveDate DATETIME2,
    --Status NVARCHAR(10) NOT NULL CHECK (Status IN('Online', 'Offline', 'Readonly'))
    ActivityStatus TINYINT NOT NULL,
    Avatar NVARCHAR(2038)
)
GO

CREATE TABLE chats(
    Id UNIQUEIDENTIFIER CONSTRAINT PK_chats_Id PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    Title NVARCHAR(32) NOT NULL,
    OwnerId UNIQUEIDENTIFIER CONSTRAINT FK_chats_OwnerId_users_Id FOREIGN KEY (OwnerId) REFERENCES users(Id) NOT NULL,
    IsPersonal BIT NOT NULL
)
GO

CREATE TABLE messages(
    Id UNIQUEIDENTIFIER CONSTRAINT PK_messages_Id PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    DispatchDate DATETIME2 NOT NULL,
	MessageText NVARCHAR(2038),
    Type TINYINT NOT NULL,
    ContentUri NVARCHAR(2038)
)
GO

CREATE TABLE contactsLists(
    Id UNIQUEIDENTIFIER CONSTRAINT PK_contactsLists_Id PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    OwnerId UNIQUEIDENTIFIER CONSTRAINT FK_contactsLists_OwnerId_users_Id FOREIGN KEY (OwnerId) REFERENCES users(Id) NOT NULL,
    UserId UNIQUEIDENTIFIER CONSTRAINT FK_contactsLists_UserId_users_Id FOREIGN KEY (UserId) REFERENCES users(Id) NOT NULL,
    --BlockageStatus BIT NOT NULL
    IsBlocked BIT NOT NULL
)
GO

CREATE TABLE chatsParticipants(
    Id UNIQUEIDENTIFIER CONSTRAINT PK_chatsParticipants_Id PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    ChatId UNIQUEIDENTIFIER CONSTRAINT FK_chatsParticipants_ChatId_chats_Id FOREIGN KEY (ChatId) REFERENCES chats(Id) NOT NULL,
    UserId UNIQUEIDENTIFIER CONSTRAINT FK_chatsParticipants_UserId_users_Id FOREIGN KEY (UserId) REFERENCES users(Id) NOT NULL,   
)
GO

CREATE TABLE messagesInChats(
    Id UNIQUEIDENTIFIER CONSTRAINT PK_messagesInChats_Id PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    MessageId UNIQUEIDENTIFIER CONSTRAINT FK_messagesInChats_MessageId_messages_Id FOREIGN KEY (MessageId) REFERENCES messages(Id) NOT NULL,
    ChatId UNIQUEIDENTIFIER CONSTRAINT FK_messagesInChats_ChatId_chats_Id FOREIGN KEY (ChatId) REFERENCES chats(Id) NOT NULL,
    FromUserId UNIQUEIDENTIFIER CONSTRAINT FK_messagesInChats_FromUserId_users_Id FOREIGN KEY (FromUserId) REFERENCES users(Id) NOT NULL,
    ToUserId UNIQUEIDENTIFIER CONSTRAINT FK_messagesInChats_ToUserId_users_Id FOREIGN KEY (ToUserId) REFERENCES users(Id) NOT NULL,
    --ReadStatus BIT NOT NULL
    IsRead BIT NOT NULL
)
GO
