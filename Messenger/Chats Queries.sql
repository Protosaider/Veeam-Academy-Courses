-- get all chats via participantId
SELECT DISTINCT 
    c.Id 
    --[Id] = c.Id
    ,
    c.Title
    --[Title] = c.Title 
    , 
    c.OwnerId 
    --[OwnerId] = c.OwnerId 
    , 
    c.IsPersonal 
    --[IsPersonal] = c.IsPersonal   
FROM usersInChat u 

INNER JOIN chats c 

ON u.ChatId = c.Id
    WHERE u.UserId = N'DCF534D2-0A40-E911-9845-78843CFE6B4B';    
    --WHERE u.UserId = @UserId;

-- get all chats by OwnerId
SELECT 
    [Id] = Id, 
    [Title] = Title, 
    [OwnerId] = OwnerId, 
    [IsPersonal] = IsPersonal 
FROM chats AS c 
    WHERE c.OwnerId = N'DCF534D2-0A40-E911-9845-78843CFE6B4B';      
    --WHERE c.OwnerId = @OwnerId

-- get chat by id
SELECT 
    [Id] = Id, 
    [Title] = Title, 
    [OwnerId] = OwnerId, 
    [IsPersonal] = IsPersonal 
FROM chats AS c 
    WHERE c.Id = N'A18C057B-0E40-E911-9845-78843CFE6B42'
    --WHERE c.Id = @Id