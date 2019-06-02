using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientApp.Controls.Contact
{
    /// <summary>
    /// Interaction logic for ContactListControl.xaml
    /// </summary>
    public partial class ContactListControl : UserControl
    {
        public ContactListControl()
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

        private void FrameworkElement_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            item.IsSelected = true;
        }
    }
}
