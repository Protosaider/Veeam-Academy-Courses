﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

// Taken from angelsix (Luke Malpass)
// Source: https://github.com/angelsix/fasetto-word/blob/develop/Source/Fasetto.Word/Window/WindowResizer.cs

namespace ClientApp.WindowHelpers
{
	// ReSharper disable once InconsistentNaming
	internal enum WindowDockPosition
    {
        Undocked = 0,
        Left = 1,
        Right = 2,
        TopBottom = 3,
        TopLeft = 4,
        TopRight = 5,
        BottomLeft = 6,
        BottomRight = 7,
    }

    /// <summary>
    /// Fixes the issue with Windows of Style <see cref="WindowStyle.None"/> covering the taskbar
    /// </summary>
	internal sealed class WindowResizer
    {
        #region Private Members

        /// <summary>
        /// The window to handle the resizing for
        /// </summary>
        private readonly System.Windows.Window _window;

		/// <summary>
        /// How close to the edge the window has to be to be detected as at the edge of the screen
        /// </summary>
        private readonly Int32 _edgeTolerance = 1;

        /// <summary>
        /// The transform matrix used to convert WPF sizes to screen pixels
        /// </summary>
        private DpiScale? _monitorDpi;

        /// <summary>
        /// The last screen the window was on
        /// </summary>
        private IntPtr _lastScreen;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition _lastDock = WindowDockPosition.Undocked;

        /// <summary>
        /// A flag indicating if the window is currently being moved/dragged
        /// </summary>
        private Boolean _beingMoved;

        #endregion

        #region Dll Imports

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
		private static extern Boolean GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
		private static extern Boolean GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        [DllImport("user32.dll")]
		private static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorOptions dwFlags);
        #endregion

        #region Public Events

        /// <summary>
        /// Called when the window dock position changes
        /// </summary>
        public event Action<WindowDockPosition> WindowDockChanged = (dock) => { };

        /// <summary>
        /// Called when the window starts being moved/dragged
        /// </summary>
        public event Action WindowStartedMove = () => { };

        /// <summary>
        /// Called when the window has been moved/dragged and then finished
        /// </summary>
        public event Action WindowFinishedMove = () => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// The size and position of the current monitor the window is on
        /// </summary>
        public Rectangle CurrentMonitorSize { get; set; }

        /// <summary>
        /// The margin around the window for the current window to compensate for any non-usable area
        /// such as the task bar
        /// </summary>
        public Thickness CurrentMonitorMargin { get; private set; }

		/// <summary>
		/// The size and position of the current screen in relation to the multi-screen desktop
		/// For example a second monitor on the right will have a Left position of
		/// the X resolution of the screens on the left
		/// </summary>
        public Rect CurrentScreenSize { get; private set; }

		#endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window">The window to monitor and correctly maximize</param>
        public WindowResizer(System.Windows.Window window)
        {
            _window = window;

            // Listen out for source initialized to setup
            _window.SourceInitialized += Window_SourceInitialized;

            // Monitor for edge docking
            _window.SizeChanged += Window_SizeChanged;
            _window.LocationChanged += Window_LocationChanged;
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize and hook into the windows message pump
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SourceInitialized(Object sender, System.EventArgs e)
        {
            // Get the handle of this window
            var handle = (new WindowInteropHelper(_window)).Handle;
            var handleSource = HwndSource.FromHwnd(handle);

            // If not found, end
			// Hook into it's Windows messages
			handleSource?.AddHook(WindowProc);
		}

        #endregion

        #region Edge Docking


        /// <summary>
        /// Monitor for moving of the window and constantly check for docked positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_LocationChanged(Object sender, EventArgs e)
        {
            Window_SizeChanged(null, null);
        }

        /// <summary>
        /// Monitors for size changes and detects if the window has been docked (Aero snap) to an edge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(Object sender, SizeChangedEventArgs e)
        {
            // Make sure our monitor info is up-to-date
            WmGetMinMaxInfo(IntPtr.Zero, IntPtr.Zero);

            // Get the monitor transform for the current position
            _monitorDpi = VisualTreeHelper.GetDpi(_window);

            // We cannot find positioning until the window transform has been established
            if (_monitorDpi == null)
                return;

            // Get window rectangle
            var top = _window.Top;
            var left = _window.Left;
            var bottom = top + _window.Height;
            var right = left + _window.Width;

            // Get window position/size in device pixels
            var windowTopLeft = new Point(left * _monitorDpi.Value.DpiScaleX, top * _monitorDpi.Value.DpiScaleX);
            var windowBottomRight = new Point(right * _monitorDpi.Value.DpiScaleX, bottom * _monitorDpi.Value.DpiScaleX);

            // Check for edges docked
            var edgedTop = windowTopLeft.Y <= (CurrentScreenSize.Top + _edgeTolerance) && windowTopLeft.Y >= (CurrentScreenSize.Top - _edgeTolerance);
            var edgedLeft = windowTopLeft.X <= (CurrentScreenSize.Left + _edgeTolerance) && windowTopLeft.X >= (CurrentScreenSize.Left - _edgeTolerance);
            var edgedBottom = windowBottomRight.Y >= (CurrentScreenSize.Bottom - _edgeTolerance) && windowBottomRight.Y <= (CurrentScreenSize.Bottom + _edgeTolerance);
            var edgedRight = windowBottomRight.X >= (CurrentScreenSize.Right - _edgeTolerance) && windowBottomRight.X <= (CurrentScreenSize.Right + _edgeTolerance);


            // Get docked position
            var dock = WindowDockPosition.Undocked;

            // Left docking
            if (edgedTop && edgedBottom && edgedLeft)
                dock = WindowDockPosition.Left;
            // Right docking
            else if (edgedTop && edgedBottom && edgedRight)
                dock = WindowDockPosition.Right;
            // Top/bottom
            else if (edgedTop && edgedBottom)
                dock = WindowDockPosition.TopBottom;
            // Top-left
            else if (edgedTop && edgedLeft)
                dock = WindowDockPosition.TopLeft;
            // Top-right
            else if (edgedTop && edgedRight)
                dock = WindowDockPosition.TopRight;
            // Bottom-left
            else if (edgedBottom && edgedLeft)
                dock = WindowDockPosition.BottomLeft;
            // Bottom-right
            else if (edgedBottom && edgedRight)
                dock = WindowDockPosition.BottomRight;
            // None
            else
                dock = WindowDockPosition.Undocked;

            // If dock has changed
            if (dock != _lastDock)
                // Inform listeners
                WindowDockChanged(dock);

            // Save last dock position
            _lastDock = dock;
        }

        #endregion

        #region Windows Message Pump (Windows Proc)

        /// <summary>
        /// Listens out for all windows messages for this window
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WindowProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            switch (msg)
            {
                // Handle the GetMinMaxInfo of the Window
                case 0x0024: // WM_GETMINMAXINFO
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;

                // Once the window starts being moved
                case 0x0231: // WM_ENTERSIZEMOVE
                    _beingMoved = true;
                    WindowStartedMove();
                    break;

                // Once the window has finished being moved
                case 0x0232: // WM_EXITSIZEMOVE
                    _beingMoved = false;
                    WindowFinishedMove();
                    break;
            }

            return (IntPtr)0;
        }

        #endregion

        /// <summary>
        /// Get the min/max window size for this window
        /// Correctly accounting for the taskbar size and position
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        private void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {
            // Get the point position to determine what screen we are on
            POINT lMousePosition;
            GetCursorPos(out lMousePosition);


            // Now get the current screen
            var lCurrentScreen = _beingMoved ?
                // If being dragged get it from the mouse position
                MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONULL) :
                // Otherwise get it from the window position (for example being moved via Win + Arrow)
                // in case the mouse is on another monitor
                MonitorFromWindow(hwnd, MonitorOptions.MONITOR_DEFAULTTONULL);

            // Try and get the current screen
            var lCurrentScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lCurrentScreen, lCurrentScreenInfo) == false)
                return;



            // Get the primary monitor at cursor position 0,0
            var lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);

            // Try and get the primary screen information
            var lPrimaryScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
                return;

            // NOTE: Always update it
            _monitorDpi = VisualTreeHelper.GetDpi(_window);

            // Store last know screen
            _lastScreen = lCurrentScreen;


            // Get work area sizes and rations
            var currentX = lCurrentScreenInfo.RCWork.Left - lCurrentScreenInfo.RCMonitor.Left;
            var currentY = lCurrentScreenInfo.RCWork.Top - lCurrentScreenInfo.RCMonitor.Top;
            var currentWidth = (lCurrentScreenInfo.RCWork.Right - lCurrentScreenInfo.RCWork.Left);
            var currentHeight = (lCurrentScreenInfo.RCWork.Bottom - lCurrentScreenInfo.RCWork.Top);
            var currentRatio = (Single)currentWidth / (Single)currentHeight;

            var primaryX = lPrimaryScreenInfo.RCWork.Left - lPrimaryScreenInfo.RCMonitor.Left;
            var primaryY = lPrimaryScreenInfo.RCWork.Top - lPrimaryScreenInfo.RCMonitor.Top;
            var primaryWidth = (lPrimaryScreenInfo.RCWork.Right - lPrimaryScreenInfo.RCWork.Left);
            var primaryHeight = (lPrimaryScreenInfo.RCWork.Bottom - lPrimaryScreenInfo.RCWork.Top);
            var primaryRatio = (Single)primaryWidth / (Single)primaryHeight;


            if (lParam != IntPtr.Zero)
            {
                // Get min/max structure to fill with information
                var lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

                // NOTE: rcMonitor is the monitor size
                //       rcWork is the available screen size (so the area inside the task bar start menu for example)

                // Size limits (used by Windows when maximized)
                // relative to 0,0 being the current screens top-left corner

                // Set to primary monitor size
                lMmi.PointMaxPosition.X = lPrimaryScreenInfo.RCMonitor.Left;
                lMmi.PointMaxPosition.Y = lPrimaryScreenInfo.RCMonitor.Top;
                lMmi.PointMaxSize.X = lPrimaryScreenInfo.RCMonitor.Right;
                lMmi.PointMaxSize.Y = lPrimaryScreenInfo.RCMonitor.Bottom;

                // Set min size
                var minSize = new Point(_window.MinWidth * _monitorDpi.Value.DpiScaleX, _window.MinHeight * _monitorDpi.Value.DpiScaleX);
                lMmi.PointMinTrackSize.X = (Int32)minSize.X;
                lMmi.PointMinTrackSize.Y = (Int32)minSize.Y;

                // Now we have the max size, allow the host to tweak as needed
                Marshal.StructureToPtr(lMmi, lParam, true);
            }

            // Set monitor size
            CurrentMonitorSize = new Rectangle(currentX, currentY, currentWidth + currentX, currentHeight + currentY);

            // Get margin around window
            CurrentMonitorMargin = new Thickness(
                (lCurrentScreenInfo.RCWork.Left - lCurrentScreenInfo.RCMonitor.Left) / _monitorDpi.Value.DpiScaleX,
                (lCurrentScreenInfo.RCWork.Top - lCurrentScreenInfo.RCMonitor.Top) / _monitorDpi.Value.DpiScaleY,
                (lCurrentScreenInfo.RCMonitor.Right - lCurrentScreenInfo.RCWork.Right) / _monitorDpi.Value.DpiScaleX,
                (lCurrentScreenInfo.RCMonitor.Bottom - lCurrentScreenInfo.RCWork.Bottom) / _monitorDpi.Value.DpiScaleY
                );

            // Store new size
            CurrentScreenSize = new Rect(lCurrentScreenInfo.RCWork.Left, lCurrentScreenInfo.RCWork.Top, currentWidth, currentHeight);

        }

        /// <summary>
        /// Gets the current cursor position in screen coordinates relative to an entire multi-desktop position
        /// </summary>
        /// <returns></returns>
        public Point GetCursorPosition()
		{
			// Get mouse position
            GetCursorPos(out var lMousePosition);

            // Apply DPI scaling
			if (_monitorDpi != null)
				return new Point(lMousePosition.X / _monitorDpi.Value.DpiScaleX,
					lMousePosition.Y / _monitorDpi.Value.DpiScaleY);

			Debugger.Break();
            return new Point(lMousePosition.X, lMousePosition.Y);
		}
    }

    #region Dll Helper Structures

	internal enum MonitorOptions : UInt32
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal sealed class MONITORINFO
    {
#pragma warning disable IDE1006 // Naming Styles
        public Int32 CBSize = Marshal.SizeOf(typeof(MONITORINFO));
        public Rectangle RCMonitor = new Rectangle();
        public Rectangle RCWork = new Rectangle();
        public Int32 DWFlags = 0;
#pragma warning restore IDE1006 // Naming Styles
    }

    [StructLayout(LayoutKind.Sequential)]
	internal struct Rectangle
    {
#pragma warning disable IDE1006 // Naming Styles
        public readonly Int32 Left;
		public readonly Int32 Top;
		public readonly Int32 Right;
		public readonly Int32 Bottom;
#pragma warning restore IDE1006 // Naming Styles

        public Rectangle(Int32 left, Int32 top, Int32 right, Int32 bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
	internal struct MINMAXINFO
    {
#pragma warning disable IDE1006 // Naming Styles
		public POINT PointReserved;
		public POINT PointMaxSize;
        public POINT PointMaxPosition;
        public POINT PointMinTrackSize;
        public POINT PointMaxTrackSize;
#pragma warning restore IDE1006 // Naming Styles
    };

    [StructLayout(LayoutKind.Sequential)]
	internal struct POINT
    {
        /// <summary>
        /// x coordinate of point.
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        public Int32 X;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// y coordinate of point.
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        public Int32 Y;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Construct a point of coordinates (x,y).
        /// </summary>
        public POINT(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public override String ToString()
        {
            return $"{X} {Y}";
        }
    }

    #endregion
}