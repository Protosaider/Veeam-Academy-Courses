using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClientApp.Animations;
using ClientApp.ViewModels.Base;

namespace ClientApp.Controls
{
	internal class BaseControl : UserControl
    {
		private EFrameworkAnimation PageLoadAnimation { get; } = EFrameworkAnimation.SlideAndFadeInFromLeft;
		private EFrameworkAnimation PageUnloadAnimation { get; } = EFrameworkAnimation.SlideAndFadeOutToBottom;

		internal Double SlideDurationInSeconds { get; } = 0.8;

		internal Boolean ShouldAnimateOut { private get; set; }

        private Object _viewModel;

		internal Object ViewModelObject
        {
            get => _viewModel;
            set
            {
                if (_viewModel == value)
                    return;
                _viewModel = value;
                // Fire the view model changed method
                OnViewModelChanged();
                DataContext = _viewModel;
            }
        }

		protected BaseControl()
        {
            // Don't bother animating in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            //firstly - If we are animating in, hide to begin with
            if (PageLoadAnimation != EFrameworkAnimation.None)
                Visibility = Visibility.Collapsed;

            //When window is loaded - start the animation
            Loaded += BasePage_LoadedAsync;
        }

        private async void BasePage_LoadedAsync(Object sender, RoutedEventArgs e)
        {
            //If set up to animate 
            if (ShouldAnimateOut)
                await AnimateOutAsync();
            else
                await AnimateInAsync();
        }

		private async Task AnimateInAsync()
        {
            if (PageLoadAnimation == EFrameworkAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case EFrameworkAnimation.SlideAndFadeInFromRight:
                    // Start the animation
					if (Application.Current.MainWindow != null)
						await this.SlideAndFadeInAsync(EAnimationSlideInDirection.Right, false, SlideDurationInSeconds,
							size: (Int32)Application.Current.MainWindow.Width);

					break;

                case EFrameworkAnimation.SlideAndFadeInFromLeft:
                    // Start the animation
					if (Application.Current.MainWindow != null)
						await this.SlideAndFadeInAsync(EAnimationSlideInDirection.Left, false, SlideDurationInSeconds,
							size: (Int32)Application.Current.MainWindow.Width);

					break;

				default:
                    throw new ArgumentOutOfRangeException();
            }
        }

		private async Task AnimateOutAsync()
        {
            if (PageUnloadAnimation == EFrameworkAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case EFrameworkAnimation.SlideAndFadeOutToLeft:
                    await this.SlideAndFadeOutAsync(EAnimationSlideInDirection.Left, SlideDurationInSeconds);
                    break;
                case EFrameworkAnimation.SlideAndFadeOutToBottom:
                    await this.SlideAndFadeOutAsync(EAnimationSlideInDirection.Bottom, SlideDurationInSeconds);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnViewModelChanged()
        {

        }
    }

	internal class BaseControl<TViewModel> : BaseControl where TViewModel : BaseViewModel, new()
    {
        private TViewModel _viewModel;

		private TViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel == value)
                    return;
                _viewModel = value;
                DataContext = _viewModel;
            }
        }

		protected BaseControl()
		{
            // If in design time mode...
            if (DesignerProperties.GetIsInDesignMode(this))
                // Just use a new instance of the VM
                ViewModel = new TViewModel();
            else
                // Create a default view model
                //TODO  We can use DI here (fore example ninject or autofac)
                //ViewModel = Framework.Service<VM>() ?? new VM();
                ViewModel = new TViewModel();
        }

        // Constructor with specific view model
		protected BaseControl(TViewModel specificViewModel = null)
		{
            if (specificViewModel != null)
            {
                ViewModel = specificViewModel;
            }
            else
            {
                // If in design time mode...
                if (DesignerProperties.GetIsInDesignMode(this))
                    // Just use a new instance of the VM
                    ViewModel = new TViewModel();
                else
                {
                    //TODO  We can use DI here (fore example ninject or autofac)
                    ViewModel = new TViewModel();
                }
            }
        }

    }
}
