using Info;
using System;
using System.Collections.Generic;

namespace DataStorage.Test
{
    public static class SWholeRepositoryStub
    {
		internal static IReadOnlyList<CChatInfo> Chats { get; private set; }
		private static IReadOnlyList<CChatsParticipantInfo> ChatsParticipants { get; set; }
		private static IReadOnlyList<CContactInfo> Contacts { get; set; }
		private static IReadOnlyList<CMessageInChatInfo> MessagesInChats { get; set; }
        public static IReadOnlyList<CMessageInfo> Messages { get; private set; }
		internal static IReadOnlyList<CUserInfo> Users { get; private set; }

        static SWholeRepositoryStub()
        {
            ResetRepos();
        }

		private static void ResetRepos()
        {
            #region Chats
            Chats = new List<CChatInfo>
            {
                new CChatInfo(Guid.Parse("A08C057B-0E40-E911-9845-78843CFE6B41"), "ChatName", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), true, 0),
                new CChatInfo(Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), "GingerRed", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false, 1),
                new CChatInfo(Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), "Transistor", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4D"), true, 0),
                new CChatInfo(Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B44"), "Furi", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true, 1),
            };
            #endregion

            #region ChatsParticipants
            ChatsParticipants = new List<CChatsParticipantInfo>
            {
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B41"), Guid.Parse("A08C057B-0E40-E911-9845-78843CFE6B41"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B42"), Guid.Parse("A08C057B-0E40-E911-9845-78843CFE6B41"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4D")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B43"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B44"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B45"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B46"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B47"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B48"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B44"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
                new CChatsParticipantInfo(Guid.Parse("8072E947-0A40-E911-9845-78843CFE6B49"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B44"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
            };
            #endregion

            #region Contacts
            Contacts = new List<CContactInfo>
            {
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B41"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4D"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B44"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B45"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B46"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B47"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false),
                new CContactInfo(Guid.Parse("F1ADB38E-1540-E911-9845-78843CFE6B48"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4D"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), false),
            };
            #endregion

            #region MessagesInChats
            MessagesInChats = new List<CMessageInChatInfo>
            {
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C18"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C15"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C19"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C15"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C20"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C16"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C21"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C16"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C22"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C17"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C23"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C17"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C24"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C18"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C25"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C18"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C26"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C19"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C27"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C19"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B43"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C28"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C20"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C29"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C20"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C30"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C21"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C31"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C21"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C32"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C22"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C33"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C22"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C34"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C23"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C35"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C23"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C36"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C24"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C37"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C24"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C38"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C25"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), false),
                new CMessageInChatInfo(Guid.Parse("8072E947-0A40-E911-B163-4C8093451C39"), Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C25"), Guid.Parse("A18C057B-0E40-E911-9845-78843CFE6B42"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), true),
        };
            #endregion

            #region Messages
    //        Messages = new List<CMessageInfo>
    //        {
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C15"),  new DateTimeOffset(2019, 05, 19, 5, 30, 10, 10, TimeSpan.FromHours(3)), "Hello, B!", 
    //0, "URI", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C16"),  new DateTimeOffset(2019, 05, 19, 5, 31, 10, 10, TimeSpan.FromHours(3)), "Hello, C!", 
    //0, "URI", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C17"),  new DateTimeOffset(2019, 05, 19, 5, 32, 10, 10, TimeSpan.FromHours(3)), "Hi, B!", 
    //0, "URI", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C18"),  new DateTimeOffset(2019, 05, 19, 5, 33, 10, 10, TimeSpan.FromHours(3)), "Hi, C!", 
    //0, "URI", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C19"),  new DateTimeOffset(2019, 05, 19, 5, 34, 10, 10, TimeSpan.FromHours(3)), "Hi = 2, B!", 
    //0, "URI", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C20"),  new DateTimeOffset(2019, 05, 19, 5, 35, 10, 10, TimeSpan.FromHours(3)), "Hi again, C!", 
    //0, "URI", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C21"),  new DateTimeOffset(2019, 05, 19, 5, 36, 10, 10, TimeSpan.FromHours(3)), "Hi again, B!", 
    //0, "URI", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C22"),  new DateTimeOffset(2019, 05, 19, 5, 37, 10, 10, TimeSpan.FromHours(3)), "Very very long message for you, B!", 
    //0, "URI", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C23"),  new DateTimeOffset(2019, 05, 19, 5, 38, 10, 10, TimeSpan.FromHours(3)), "B!", 
    //0, "URI", Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C24"),  new DateTimeOffset(2019, 05, 19, 5, 39, 10, 10, TimeSpan.FromHours(3)), "Very very long message for you, C!", 
    //0, "URI", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B")),
    //new CMessageInfo(Guid.Parse("3C3BBA0A-9842-E911-B163-4C8093451C25"), new DateTimeOffset(2019, 05, 19, 5, 40, 10, 10, TimeSpan.FromHours(3)), "C!", 
    //0, "URI", Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"))
    //        };
            #endregion

            #region Users
            Users = new List<CUserInfo>
            {             
                new CUserInfo(Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4A"), "alpha", "alpha", new DateTimeOffset(2019, 05, 19, 5, 45, 10, 10, TimeSpan.FromHours(3)), 0, "URI"),
                new CUserInfo(Guid.Parse("DCF534D2-0A40-E911-9845-78843CFE6B4B"), "bravo", "bravo", new DateTimeOffset(2019, 05, 19, 5, 50, 10, 10, TimeSpan.FromHours(3)), 0, "URI"),
                new CUserInfo(Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4C"), "charlie", "charlie", new DateTimeOffset(2019, 05, 19, 6, 30, 10, 10, TimeSpan.FromHours(3)), 0, "URI"),
                new CUserInfo(Guid.Parse("DDF534D2-0A40-E911-9845-78843CFE6B4D"), "delta", "delta", new DateTimeOffset(2019, 05, 19, 5, 55, 10, 10, TimeSpan.FromHours(3)), 0, "URI"),
            };
            #endregion
        }
    }
}
