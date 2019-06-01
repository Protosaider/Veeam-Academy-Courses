using ClientApp.ViewModels.ChatPage;
using ClientApp.ViewModels.Contact;
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

namespace ClientApp.Controls.Contact
{
    /// <summary>
    /// Interaction logic for ChatListHolderControl.xaml
    /// </summary>
    public partial class ContactListHolderControl : BaseControl<ContactListViewModel>
    {
        public ContactListHolderControl() : base()
        {
            InitializeComponent();
        }

        public ContactListHolderControl(ContactListViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
