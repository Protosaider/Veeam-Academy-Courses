using ClientApp.ViewModels.ContactAdd;
using System;
using System.Windows.Input;

namespace ClientApp.Controls.Contact
{
    /// <summary>
    /// Interaction logic for ContactAddControl.xaml
    /// </summary>
	internal partial class ContactAddControl : BaseControl<ContactAddListViewModel>
    {
        public ContactAddControl()
		{
            InitializeComponent();
        }

        public ContactAddControl(ContactAddListViewModel vm) : base(vm)
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
