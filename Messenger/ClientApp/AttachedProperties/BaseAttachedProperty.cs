using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.AttachedProperties
{
    /// <summary>
    /// A base attached property to replace the vanilla WPF attached property
    /// </summary>
    /// <typeparam name="TParent">The parent class to be the attached property</typeparam>
    /// <typeparam name="TProperty">The type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<TParent, TProperty> where TParent : new()
    {
        //Singletone instance
		private static TParent Instance { get; } = new TParent();

        #region Attached Property definition


        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(TProperty),
            typeof(BaseAttachedProperty<TParent, TProperty>), new UIPropertyMetadata(default(TProperty),
                new PropertyChangedCallback(OnValuePropertyChanged), new CoerceValueCallback(OnValuePropertyUpdated)));

        public static TProperty GetValue(DependencyObject d) => (TProperty)d.GetValue(ValueProperty);
        public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">The UI element that had it's property changed</param>
        /// <param name="e">The arguments for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<TParent, Boolean>).OnValueChanged(d, e);

            // Call event listeners
            (Instance as BaseAttachedProperty<TParent, Boolean>).ValueChanged?.Invoke(d, e);
        }

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed, even if it is the same value (useful for animations)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static Object OnValuePropertyUpdated(DependencyObject d, Object baseValue)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<TParent, Boolean>).OnValueUpdated(d, baseValue);

            // Call event listeners
            (Instance as BaseAttachedProperty<TParent, Boolean>).ValueUpdated?.Invoke(d, baseValue);

            return baseValue;
        }

        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="e">The arguments for this event</param>
		protected virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

		protected virtual void OnValueUpdated(DependencyObject sender, Object baseValue)
        {

        }

        #endregion

        // Fired when the value changes
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged;

        public event Action<DependencyObject, Object> ValueUpdated;

    }
}
