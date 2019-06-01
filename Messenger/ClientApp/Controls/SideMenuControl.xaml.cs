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
using ClientApp.Pages;

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
