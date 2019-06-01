using System;
using System.Diagnostics;
using ClientApp.Controls;
using ClientApp.Controls.Chat;
using ClientApp.Controls.ChatMessage;
using ClientApp.Controls.Contact;
using ClientApp.Pages;
using ClientApp.ViewModels;
using ClientApp.ViewModels.ChatPage;
using ClientApp.ViewModels.Contact;
using ClientApp.ViewModels.ContactAdd;

namespace ClientApp.Other
{
    /// <summary>
    /// Converts the <see cref="SideMenuContent"/> to an actual view/page
    /// </summary>
	internal static class SSideMenuContentHelpers
    {
        /// <summary>
        /// Takes a <see cref="SideMenuContent"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="content"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BaseControl ToBaseControl(this SideMenuContent content, Object viewModel = null)
        {
            // Find the appropriate page
            switch (content)
            {
                case SideMenuContent.None:
                    return null;

                case SideMenuContent.Chats:
                    //return new ChatListControl(viewModel as ChatListViewModel);
                    return new ChatListHolderControl(viewModel as ChatListViewModel);

                case SideMenuContent.Contacts:
                    return new ContactListHolderControl(viewModel as ContactListViewModel);

                case SideMenuContent.AddContact:
                    return new ContactAddControl(viewModel as ContactAddListViewModel);

                case SideMenuContent.AddChat:
                    return new ChatCreateControl(viewModel as ChatCreateViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BaseControl"/> to the specific <see cref="SideMenuContent"/> that is for that type of page
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static SideMenuContent ToSideMenuContent(this BaseControl control)
        {
            // Find application page that matches the base page
            //if (control is ChatListControl)
            //    return SideMenuContent.Chats;
            if (control is ChatListHolderControl)
                return SideMenuContent.Chats;

            if (control is ContactListHolderControl)
                return SideMenuContent.Contacts;

            if (control is ContactAddControl)
                return SideMenuContent.AddContact;

            if (control is ChatCreateControl)
                return SideMenuContent.AddChat;

            // Alert developer of issue
            Debugger.Break();
            return default(SideMenuContent);
        }
    }
}
