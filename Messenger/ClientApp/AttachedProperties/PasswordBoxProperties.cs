using System;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.AttachedProperties
{
    public sealed class HasTextProperty : BaseAttachedProperty<HasTextProperty, Boolean>
    {
		internal static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }

    //Assumed that UI element already has attached property HasTextProperty
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, Boolean>
    {
		protected override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller
            var passwordBox = sender as PasswordBox;

            // Make sure it is a password box
            if (passwordBox == null)
                return;

            // Remove any previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // If the caller set MonitorPassword to true...
            if ((Boolean)e.NewValue)
            {
                // Set default value
                HasTextProperty.SetValue(passwordBox);
                // Start listening out for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private void PasswordBox_PasswordChanged(Object sender, RoutedEventArgs e)
        {
            // Set the attached HasText value
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

}
