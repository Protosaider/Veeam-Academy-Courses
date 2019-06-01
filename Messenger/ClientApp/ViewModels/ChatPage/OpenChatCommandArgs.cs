using ClientApp.DataSuppliers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels.ChatPage
{
    public class OpenChatCommandArgs
    {
        public readonly CChatData ChatData;
        public readonly Boolean IsSelected;

        public OpenChatCommandArgs(CChatData chatData, Boolean isSelected)
        {
            ChatData = chatData;
            IsSelected = isSelected;
        }
    }
}
