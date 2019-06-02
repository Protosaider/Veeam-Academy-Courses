using ClientApp.DataSuppliers.Data;
using System;

namespace ClientApp.ViewModels.ChatPage
{
	internal class COpenChatCommandArgs
    {
		public CChatData ChatData { get; }
		public Boolean IsSelected { get; }

		public COpenChatCommandArgs(CChatData chatData, Boolean isSelected)
        {
            ChatData = chatData;
            IsSelected = isSelected;
        }
    }
}
