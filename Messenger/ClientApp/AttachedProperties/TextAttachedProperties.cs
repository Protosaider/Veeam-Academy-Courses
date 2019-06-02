using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ClientApp.AttachedProperties
{
    /// <summary>
    /// Focuses (keyboard focus) this element on load
    /// </summary>
    public sealed class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, Boolean>
    {
		protected override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control))
                return;

            control.Loaded += (s, ee) => control.Focus();
        }
    }

    public sealed class FocusProperty : BaseAttachedProperty<FocusProperty, Boolean>
    {
		protected override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control))
                return;

            if ((Boolean)e.NewValue)
                control.Focus();
        }
    }

    public class FocusAndSelectProperty : BaseAttachedProperty<FocusAndSelectProperty, Boolean>
    {
		protected override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If we don't have a control, return
            if (sender is TextBoxBase control)
            {
                if ((Boolean)e.NewValue)
                {
                    control.Focus();

                    control.SelectAll();
                }
            }

            if (sender is PasswordBox password)
            {
                if ((Boolean)e.NewValue)
                {
                    password.Focus();

                    password.SelectAll();
                }
            }
        }
    }
}
