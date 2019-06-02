using System.Windows.Controls;

namespace ClientApp.Controls
{
    /// <summary>
    /// Interaction logic for SideMenuControl.xaml
    /// </summary>
    public partial class SideMenuControl : UserControl
    {
        //public BaseViewModel CurrentPageViewModel
        //{
        //    get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
        //    set => SetValue(CurrentPageViewModelProperty, value);
        //}

        //public static readonly DependencyProperty CurrentPageViewModelProperty =
        //    DependencyProperty.Register(nameof(CurrentPageViewModel), typeof(BaseViewModel), typeof(SideMenuControl),
        //        new UIPropertyMetadata(default(BaseViewModel), null, CurrentPageViewModelPropertyChanged));


        //private static Object CurrentPageViewModelPropertyChanged(DependencyObject d, Object value)
        //{
        //    // Get current values
        //    var viewModelToSet = (BaseViewModel)value;

        //    //get frames from xaml
        //    var newControl = (d as SideMenuControl).CurControl;

        //    // If the current page hasn't changed just update the view model
        //    if (newControl.Content is BasePage page)
        //    {
        //        // Just update the view model
        //        page.ViewModelObject = viewModelToSet;
        //        return value;
        //    }

        //    return value;
        //}


        public SideMenuControl()
        {
            InitializeComponent();
        }


    }
}
