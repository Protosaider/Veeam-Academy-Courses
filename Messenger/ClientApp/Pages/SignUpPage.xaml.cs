using ClientApp.ViewModels.SignUpPage;

namespace ClientApp.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : BasePage<CSignUpViewModel>
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        public SignUpPage(CSignUpViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
