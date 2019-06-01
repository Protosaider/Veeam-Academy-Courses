using ClientApp.ViewModels;
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
using ClientApp.ViewModels.LogInPage;
using System.Security;

namespace ClientApp.Pages
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    //public partial class LogInPage : BasePage<CLogInViewModel>, Other.IHavePassword
    public partial class LogInPage : BasePage<CLogInViewModel>
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        public LogInPage(CLogInViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        /// The secure password for this page
        //public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
