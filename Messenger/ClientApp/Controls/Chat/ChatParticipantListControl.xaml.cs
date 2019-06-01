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
    /// Interaction logic for ChatParticipantListControl.xaml
    /// </summary>
    public partial class ChatParticipantListControl : UserControl
    {
        public ChatParticipantListControl()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewKeyDown(Object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Right:
                case Key.Up:
                case Key.Down:
                case Key.Tab:
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
