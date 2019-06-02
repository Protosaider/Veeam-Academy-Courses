using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.AttachedProperties
{

    public sealed class ScrollToBottomOnLoadProperty : BaseAttachedProperty<ScrollToBottomOnLoadProperty, Boolean>
    {
		protected override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Don't do this in design time
            if (DesignerProperties.GetIsInDesignMode(sender))
                return;

            // If we don't have a control, return
            if (!(sender is ScrollViewer control))
                return;

            // Scroll content to bottom when context changes
            control.DataContextChanged -= Control_DataContextChanged;
            control.DataContextChanged += Control_DataContextChanged;
        }

        private void Control_DataContextChanged(Object sender, DependencyPropertyChangedEventArgs e) => 
            (sender as ScrollViewer)?.ScrollToBottom();
    }

    /// <summary>
    /// Automatically keep the scroll at the bottom of the screen when we are already close to the bottom
    /// </summary>
    public sealed class AutoScrollToBottomProperty : BaseAttachedProperty<AutoScrollToBottomProperty, Boolean>
    {
		protected override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Don't do this in design time
            if (DesignerProperties.GetIsInDesignMode(sender))
                return;

            // If we don't have a control, return
            if (!(sender is ScrollViewer control))
                return;

            // Scroll content to bottom when context changes
            control.ScrollChanged -= Control_ScrollChanged;
            control.ScrollChanged += Control_ScrollChanged;
        }

        private void Control_ScrollChanged(Object sender, ScrollChangedEventArgs e)
        {
			// If we are close enough to the bottom...
            if (sender is ScrollViewer scroll && scroll.ScrollableHeight - scroll.VerticalOffset < 20)
                // Scroll to the bottom
                scroll.ScrollToEnd();
        }
    }
}
