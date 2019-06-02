using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClientApp.Other;
using ClientApp.Pages;
using ClientApp.ViewModels.Base;

namespace ClientApp.Controls
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {

        public EApplicationPage CurrentPage
        {
            get => (EApplicationPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(EApplicationPage), typeof(PageHost),
                new UIPropertyMetadata(default(EApplicationPage), null, CurrentPagePropertyChanged));

        public BaseViewModel CurrentPageViewModel
        {
            get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
            set => SetValue(CurrentPageViewModelProperty, value);
        }

        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel), typeof(BaseViewModel), typeof(PageHost),
                new UIPropertyMetadata());

        public PageHost()
        {
            InitializeComponent();

            // If we are in DesignMode, show the current page
            // as the dependency property does not fire
            if (DesignerProperties.GetIsInDesignMode(this))
                //NewPage.Content = IoC.Application.CurrentPage.ToBasePage();
                NewPage.Content = CViewModelLocator.Instance.ApplicationViewModel.CurrentPage.ToBasePage();
        }

        private static Object CurrentPagePropertyChanged(DependencyObject d, Object value)
        {
            // Get current values
            var currentPage = (EApplicationPage)value;
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

			if (!(d is PageHost))
			{
				Debugger.Break();
				return value;
			}

            //get frames from xaml
            var newPageFrame = ((PageHost)d).NewPage;
            var oldPageFrame = ((PageHost)d).OldPage;

            // If the current page hasn't changed just update the view model
            if (newPageFrame.Content is BasePage page &&
                page.ToApplicationPage() == currentPage)
            {
                // Just update the view model
                page.ViewModelObject = currentPageViewModel;

                return value;
            }

            var oldPageContent = newPageFrame.Content;
            newPageFrame.Content = null;
            oldPageFrame.Content = oldPageContent;

            //animate out previous page when the loaded event fires right after this call due to moving frames 
            if (oldPageContent is BasePage oldPage)
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

            newPageFrame.Content = currentPage.ToBasePage(currentPageViewModel);

            return value;
        }
     
    }
}
