using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NavigatingBetweenViewsMVVM.AttachedProperties
{
    /// <summary>
    /// For creating a Frame that never shows navigation and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, Boolean>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frame = sender as Frame;
            //hide navigation bar
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            //clear navigation history
            frame.Navigated += (s, ee) => ((Frame)s).NavigationService.RemoveBackEntry();
        }
    }
}
