using ClientApp.ViewModels.Base;
using ClientApp.WindowHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ClientApp.ViewModels
{
    /// <summary>
    ///  Taken from angelsix (Luke Malpass)
    /// Source: https://github.com/angelsix/fasetto-word/blob/develop/Source/Fasetto.Word/WPFViewModels/WindowViewModel.cs
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        private System.Windows.Window _window;
        private WindowDockPosition _dockPosition = WindowDockPosition.Undocked;

        // The window resizer helper that keeps the window size correct in various states
        private WindowResizer _windowResizer;

        // Size of the resize border around the window - на полном экране площадь, за которую можно потянуть должна быть внутри
        public Int32 ResizeBorder => Borderless ? 0 : 6;
        public Thickness ResizeBorderThickness => new Thickness(OuterMarginSizeThickness.Left + ResizeBorder,
                                                                OuterMarginSizeThickness.Top + ResizeBorder,
                                                                OuterMarginSizeThickness.Right + ResizeBorder,
                                                                OuterMarginSizeThickness.Bottom + ResizeBorder);
        //taking into accound the outer margin

        // Margin around window to allow for a drop shadow - создает проблемы, когда окно на полном экране - оно будто окружено пустой рамкой. Fixed in property
        private Thickness _outerMarginSize = new Thickness(5);
        public Thickness OuterMarginSizeThickness
        {
            // If it is maximized or docked, no border
            get => _window.WindowState == WindowState.Maximized ? _windowResizer.CurrentMonitorMargin : (Borderless ? new Thickness(0) : _outerMarginSize);
            set => _outerMarginSize = value;
        }

        // Radius of the edges of the window - на полном экране не надо скруглять окно
        private Int32 _windowRadius = 10;
        public Int32 WindowRadius { get => Borderless ? 0 : _windowRadius; set => _windowRadius = value; }
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        // The rectangle border around the window when docked
        public Int32 FlatBorderThickness => Borderless && _window.WindowState != WindowState.Maximized ? 1 : 0;

        //The height of the title bar / caption of the window
        public Int32 TitleHeight { get; set; } = 22;
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        //Window minimum sizes
        public Double WindowMinimumHeight { get; set; } = 500.0;
        public Double WindowMinimumWidth { get; set; } = 800.0;

        //if the window is currently being moved/dragged
        public Boolean BeingMoved { get; set; }

        public Thickness InnerContentPaddingThickness { get; set; } = new Thickness(0);

        //True if the window should be borderless because it's docked or maximized
        public Boolean Borderless => _window.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked;

        // True if we should have a dimmed overlay on the window such as when a popup is visible or the window is not focused
        public Boolean DimmableOverlayVisible { get; set; }


        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }


        public WindowViewModel(System.Windows.Window window)
        {
            _window = window;

            // Listen out for the window resizing
            _window.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            // Create commands
            MinimizeCommand = new CRelayCommand((obj) => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new CRelayCommand((obj) => _window.WindowState ^= WindowState.Maximized); //2 and 2 => 0,  0 and 2 => 2
            CloseCommand = new CRelayCommand((obj) => _window.Close());
            MenuCommand = new CRelayCommand((obj) => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

            //Fix window resize problems
            _windowResizer = new WindowResizer(_window);
            // Listen out for dock changes
            _windowResizer.WindowDockChanged += (dock) =>
            {
                _dockPosition = dock;
                WindowResized();
            };

            // On window being moved/dragged
            _windowResizer.WindowStartedMove += () =>
            {
                BeingMoved = true;
            };

            // Fix dropping an undocked window at top which should be positioned at the very top of screen
            _windowResizer.WindowFinishedMove += () =>
            {
                BeingMoved = false;

                // Check for moved to top of window and not at an edge
                if (_dockPosition == WindowDockPosition.Undocked && _window.Top == _windowResizer.CurrentScreenSize.Top)
                    // If so, move it to the true top (the border size)
                    _window.Top = -OuterMarginSizeThickness.Top;
            };
        }

        //private Point GetMousePosition() => _window.PointToScreen(Mouse.GetPosition(_window));
        private Point GetMousePosition() => _windowResizer.GetCursorPosition();

        private void WindowResized()
        {
            //Fire off events for all properties affected by resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(FlatBorderThickness));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }


    }
}
