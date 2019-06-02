using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientApp.Controls.Chat
{
    /// <summary>
    /// Interaction logic for ChatCreateListControl.xaml
    /// </summary>
    public partial class ChatCreateListControl : UserControl
    {
        public ChatCreateListControl()
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

        private void FrameworkElement_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            item.IsSelected = true;
        }
    }
}
