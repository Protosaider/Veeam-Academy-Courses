using ClientApp.ViewModels.Contact;

namespace ClientApp.Controls.Contact
{
    /// <summary>
    /// Interaction logic for ChatListHolderControl.xaml
    /// </summary>
	internal partial class ContactListHolderControl : BaseControl<ContactListViewModel>
    {
        public ContactListHolderControl()
		{
            InitializeComponent();
        }

        public ContactListHolderControl(ContactListViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
