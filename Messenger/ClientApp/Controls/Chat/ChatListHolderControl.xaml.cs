using ClientApp.ViewModels.ChatPage;

namespace ClientApp.Controls.Chat
{
    /// <summary>
    /// Interaction logic for ChatListHolderControl.xaml
    /// </summary>
	internal partial class ChatListHolderControl : BaseControl<ChatListViewModel>
    {
        public ChatListHolderControl()
		{
            InitializeComponent();
        }

        public ChatListHolderControl(ChatListViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
