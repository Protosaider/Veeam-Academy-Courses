using ClientApp.ViewModels;
using System;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WindowViewModel(this);
        }

        private void AppWindow_Deactivated(Object sender, EventArgs e)
        {
            // Show overlay if we lose focus
            ((WindowViewModel)DataContext).DimmableOverlayVisible = true;
        }

        private void AppWindow_Activated(Object sender, EventArgs e)
        {
            // Hide overlay if we are focused
            ((WindowViewModel)DataContext).DimmableOverlayVisible = false;
        }
    }
}
