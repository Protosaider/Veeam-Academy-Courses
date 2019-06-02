using ClientApp.ViewModels.ChatPage;

namespace ClientApp.Controls.Chat
{
    /// <summary>
    /// Interaction logic for ChatCreateNewControl.xaml
    /// </summary>
	internal partial class ChatCreateControl : BaseControl<ChatCreateViewModel>
    {
        public ChatCreateControl()
		{
            InitializeComponent();
        }

        public ChatCreateControl(ChatCreateViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
