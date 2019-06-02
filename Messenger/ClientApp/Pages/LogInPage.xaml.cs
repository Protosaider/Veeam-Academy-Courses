using ClientApp.ViewModels.LogInPage;

namespace ClientApp.Pages
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    //public partial class LogInPage : BasePage<CLogInViewModel>, Other.IHavePassword
	internal partial class LogInPage : BasePage<CLogInViewModel>
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        public LogInPage(CLogInViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
