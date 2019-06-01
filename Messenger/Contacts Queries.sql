-- get contact
SELECT 
    [Id] = Id,
    [OwnerId] = OwnerId,
    [UserId] = UserId,
    [IsBlocked] = IsBlocked
FROM contactsLists AS c 
    WHERE c.OwnerId = N'DCF534D2-0A40-E911-9845-78843CFE6B4A'   
    --WHERE c.OwnerId = @OwnerId

-- delete contact
DELETE FROM contactsLists 
    WHERE UserId = N'DDF534D2-0A40-E911-9845-78843CFE6B4D'
    --WHERE UserId = @UserToRemoveId 
        AND OwnerId = N'DCF534D2-0A40-E911-9845-78843CFE6B4A'        
        --AND OwnerId = @OwnerId

-- create contact
INSERT INTO contactsLists (Id, OwnerId, UserId, IsBlocked)
    VALUES (
DEFAULT, N'DDF534D2-0A40-E911-9845-78843CFE6B4C', N'DCF534D2-0A40-E911-9845-78843CFE6B4B', 0
--DEFAULT, @OwnerId, @UserId, @IsBlocked
)