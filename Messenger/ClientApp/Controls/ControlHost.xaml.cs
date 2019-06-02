using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClientApp.Other;
using ClientApp.ViewModels;
using ClientApp.ViewModels.Base;

namespace ClientApp.Controls
{
    /// <summary>
    /// Interaction logic for ControlHost.xaml
    /// </summary>
    public partial class ControlHost : UserControl
    {
        public SideMenuContent CurrentControl
        {
            get => (SideMenuContent)GetValue(CurrentControlProperty);
            set => SetValue(CurrentControlProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentControlProperty =
            DependencyProperty.Register(nameof(CurrentControl), typeof(SideMenuContent), typeof(ControlHost),
                new UIPropertyMetadata(default(SideMenuContent), null, CurrentControlPropertyChanged));

        public BaseViewModel CurrentControlViewModel
        {
            get => (BaseViewModel)GetValue(CurrentControlViewModelProperty);
            set => SetValue(CurrentControlViewModelProperty, value);
        }

        public static readonly DependencyProperty CurrentControlViewModelProperty =
            DependencyProperty.Register(nameof(CurrentControlViewModel), typeof(BaseViewModel), typeof(ControlHost),
                new UIPropertyMetadata());

        public ControlHost()
        {
            InitializeComponent();

            // If we are in DesignMode, show the current page
            // as the dependency property does not fire
            if (DesignerProperties.GetIsInDesignMode(this))
                //NewPage.Content = IoC.Application.CurrentPage.ToBasePage();
                NewControl.Content = CViewModelLocator.Instance.ApplicationViewModel.CurrentSideMenuContent.ToBaseControl();
        }

        private static Object CurrentControlPropertyChanged(DependencyObject d, Object value)
        {
            // Get current values
            var currentControl = (SideMenuContent)value;
            var currentControlViewModel = d.GetValue(CurrentControlViewModelProperty);

			if (!(d is ControlHost))
			{
				//Debugger.Break();
				return value;
            }

            //get frames from xaml
            var newPageFrame = ((ControlHost)d).NewControl;
            var oldPageFrame = ((ControlHost)d).OldControl;

            // If the current page hasn't changed just update the view model
            if (newPageFrame.Content is BaseControl page &&
                page.ToSideMenuContent() == currentControl)
            {
                // Just update the view model
                page.ViewModelObject = currentControlViewModel;
                return value;
            }

            var oldPageContent = newPageFrame.Content;
            newPageFrame.Content = null;
            oldPageFrame.Content = oldPageContent;

            //animate out previous page when the loaded event fires right after this call due to moving frames 
            if (oldPageContent is BaseControl oldPage)
            {
                // animate old page to animate out 
                oldPage.ShouldAnimateOut = true;
                // Once it is done, remove it
                Task.Delay((Int32)(oldPage.SlideDurationInSeconds * 1000)).ContinueWith((t) =>
                {
                    // Remove old page
                    // Use delay to wait for animation ending
                    // Use dispatcher because you can't do anything with UI when you are NON in UI thread
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }

            newPageFrame.Content = currentControl.ToBaseControl(currentControlViewModel);

            return value;
        }
    }
}
