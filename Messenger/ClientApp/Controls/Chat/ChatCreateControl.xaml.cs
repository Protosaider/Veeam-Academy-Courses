using ClientApp.ViewModels.ChatPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApp.Controls.Chat
{
    /// <summary>
    /// Interaction logic for ChatCreateNewControl.xaml
    /// </summary>
    public partial class ChatCreateControl : BaseControl<ChatCreateViewModel>
    {
        public ChatCreateControl() : base()
        {
            InitializeComponent();
        }

        public ChatCreateControl(ChatCreateViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
