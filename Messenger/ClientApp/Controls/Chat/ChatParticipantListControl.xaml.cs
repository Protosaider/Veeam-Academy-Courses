using System;
using System.Windows.Controls;
using System.Windows.Input;

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
			}
        }
    }
}
