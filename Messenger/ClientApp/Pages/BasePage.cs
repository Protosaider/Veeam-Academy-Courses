using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClientApp.Animations;
using ClientApp.ViewModels;
using ClientApp.ViewModels.Base;

namespace ClientApp.Pages
{
    public class BasePage : UserControl
    {
		private EFrameworkAnimation PageLoadAnimation { get; } = EFrameworkAnimation.SlideAndFadeInFromRight;
		private EFrameworkAnimation PageUnloadAnimation { get; } = EFrameworkAnimation.SlideAndFadeOutToLeft;

		internal Double SlideDurationInSeconds { get; } = 0.8;

		internal Boolean ShouldAnimateOut { get; set; }

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

		protected BasePage()
        {
            // Don't bother animating in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            //firstly - If we are animating in, hide to begin with
            if (PageLoadAnimation != EFrameworkAnimation.None)
                Visibility = System.Windows.Visibility.Collapsed;
            //When window is loaded - start the animation
            Loaded += BasePage_LoadedAsync;
        }

        private async void BasePage_LoadedAsync(Object sender, System.Windows.RoutedEventArgs e)
        {
            //Task.Run(async () => await AnimateInAsync());
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
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, SlideDurationInSeconds, size: (Int32)Application.Current.MainWindow.Width);

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
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideDurationInSeconds);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnViewModelChanged()
        {

        }
    }

    public class BasePage<TViewModel> : BasePage where TViewModel : BaseViewModel, new()
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

		protected BasePage() : base()
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
		protected BasePage(TViewModel specificViewModel = null) : base()
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

    //public class BasePage<TViewModel, TViewModelParameter> : BasePage where TViewModel : BaseViewModel<TViewModelParameter>, new()
    //{
    //    private TViewModel _viewModel;
    //    public TViewModel ViewModel
    //    {
    //        get => _viewModel;
    //        set
    //        {
    //            if (_viewModel == value)
    //                return;
    //            _viewModel = value;
    //            DataContext = _viewModel;
    //        }
    //    }

    //    public BasePage() : base()
    //    {
    //        // If in design time mode...
    //        if (DesignerProperties.GetIsInDesignMode(this))
    //            // Just use a new instance of the VM
    //            ViewModel = new TViewModel();
    //        else
    //            // Create a default view model
    //            //TODO  We can use DI here (fore example ninject or autofac)
    //            //ViewModel = Framework.Service<VM>() ?? new VM();
    //            ViewModel = new TViewModel();
    //    }

    //    // Constructor with specific view model
    //    public BasePage(TViewModel specificViewModel = null, TViewModelParameter parameter = default(TViewModelParameter))
    //    {
    //        if (specificViewModel != null)
    //        {
    //            ViewModel = specificViewModel;
    //        }
    //        else
    //        {
    //            if (DesignerProperties.GetIsInDesignMode(this))
    //            {
    //                ViewModel = new TViewModel();
    //                ViewModel.Initialize(parameter);
    //            }
    //            else
    //            {
    //                ViewModel = new TViewModel();
    //                ViewModel.Initialize(parameter);
    //            }
    //        }
    //    }
    //}

}
