using ClientApp.ViewModels.Contact;
using ClientApp.ViewModels.ContactAdd;
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
    /// Interaction logic for ContactAddControl.xaml
    /// </summary>
    public partial class ContactAddControl : BaseControl<ContactAddListViewModel>
    {
        public ContactAddControl() : base()
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
                default:
                    break;
            }
        }
    }
}
