﻿USE messenger_db
GO
SET DATEFORMAT ymd
--YYYY-MM-DD HH:MM:SS.SSSSS
GO
INSERT INTO users (Id, Login, LastActiveDate, ActivityStatus, Avatar)
    VALUES 
    (N'DCF534D2-0A40-E911-9845-78843CFE6B4A', N'alpha', SYSDATETIME(), 0, N'URI'),
    (N'DCF534D2-0A40-E911-9845-78843CFE6B4B', N'bravo', SYSDATETIME(), 0, N'URI'),
    (N'DDF534D2-0A40-E911-9845-78843CFE6B4C', N'charlie', SYSDATETIME(), 0, N'URI'),
    (N'DDF534D2-0A40-E911-9845-78843CFE6B4D', N'delta', SYSDATETIME(), 0, N'URI');
GO

INSERT INTO chats(Id, Title, OwnerId, IsPersonal)
    VALUES 
    (N'A08C057B-0E40-E911-9845-78843CFE6B41', N'ChatName', N'DCF534D2-0A40-E911-9845-78843CFE6B4A', 1),
    (N'A18C057B-0E40-E911-9845-78843CFE6B42', N'GingerRed', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', 0);
GO

INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
    VALUES 
    (N'3C3BBA0A-9842-E911-B163-4C8093451C15', SYSDATETIME(), N'Hello, B!', 0, N'URI')
GO

INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
    VALUES
    (N'3C3BBA0A-9842-E911-B163-4C8093451C16', SYSDATETIME(), N'Hello, C!', 0, N'URI')
GO

INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
    VALUES
    (N'3C3BBA0A-9842-E911-B163-4C8093451C17', SYSDATETIME(), N'Hi, B!', 0, N'URI')
GO

INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
    VALUES
    (N'3C3BBA0A-9842-E911-B163-4C8093451C18', SYSDATETIME(), N'Hi = 2, B!', 0, N'URI')
GO

INSERT INTO messages (Id, DispatchDate, MessageText, Type, ContentUri)
    VALUES
    (N'3C3BBA0A-9842-E911-B163-4C8093451C19', SYSDATETIME(), N'Hi, C!', 0, N'URI')
GO

INSERT INTO contactsLists (Id, OwnerId, UserId, IsBlocked)
    VALUES
    (N'F1ADB38E-1540-E911-9845-78843CFE6B4A', N'DCF534D2-0A40-E911-9845-78843CFE6B4A', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', 0),
    (N'F1ADB38E-1540-E911-9845-78843CFE6B4B', N'DCF534D2-0A40-E911-9845-78843CFE6B4A', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', 0),
    (N'F1ADB38E-1540-E911-9845-78843CFE6B4C', N'DCF534D2-0A40-E911-9845-78843CFE6B4A', N'DDF534D2-0A40-E911-9845-78843CFE6B4D', 1),
    (N'F1ADB38E-1540-E911-9845-78843CFE6B4D', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', N'DDF534D2-0A40-E911-9845-78843CFE6B4D', 0),
	(N'F1ADB38E-1540-E911-9845-78843CFE6B4E', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', N'DDF534D2-0A40-E911-9845-78843CFE6B4D', 0);
GO

INSERT INTO chatsParticipants (Id, ChatId, UserId)
    VALUES
    (N'8072E947-0A40-E911-9845-78843CFE6B4A', N'A08C057B-0E40-E911-9845-78843CFE6B41', N'DCF534D2-0A40-E911-9845-78843CFE6B4A'),
    (N'8072E947-0A40-E911-9845-78843CFE6B4B', N'A08C057B-0E40-E911-9845-78843CFE6B41', N'DCF534D2-0A40-E911-9845-78843CFE6B4B'),
    (N'8072E947-0A40-E911-9845-78843CFE6B4C', N'A08C057B-0E40-E911-9845-78843CFE6B41', N'DDF534D2-0A40-E911-9845-78843CFE6B4D'),
    (N'8072E947-0A40-E911-9845-78843CFE6B4D', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DDF534D2-0A40-E911-9845-78843CFE6B4C'),
    (N'8072E947-0A40-E911-9845-78843CFE6B4E', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DCF534D2-0A40-E911-9845-78843CFE6B4B'),
    (N'8072E947-0A40-E911-9845-78843CFE6B4F', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DCF534D2-0A40-E911-9845-78843CFE6B4A');
GO

INSERT INTO messagesInChats (Id, MessageId, ChatId, FromUserId, ToUserId, IsRead)
    VALUES
    (N'8072E947-0A40-E911-B163-4C8093451C18', N'3C3BBA0A-9842-E911-B163-4C8093451C15', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', 1),
    (N'8072E947-0A40-E911-B163-4C8093451C19', N'3C3BBA0A-9842-E911-B163-4C8093451C16', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', 1),
    (N'8072E947-0A40-E911-B163-4C8093451C20', N'3C3BBA0A-9842-E911-B163-4C8093451C17', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', 1),
    (N'8072E947-0A40-E911-B163-4C8093451C21', N'3C3BBA0A-9842-E911-B163-4C8093451C18', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', 1),
    (N'8072E947-0A40-E911-B163-4C8093451C22', N'3C3BBA0A-9842-E911-B163-4C8093451C19', N'A18C057B-0E40-E911-9845-78843CFE6B42', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', N'DDF534D2-0A40-E911-9845-78843CFE6B4C', 0);
GO
