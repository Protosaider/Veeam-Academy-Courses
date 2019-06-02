using ClientApp.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Standard;

namespace ClientApp.AttachedProperties
{
    /// <summary>
    /// A base class to run any animation method when a boolean is set to true
    /// and a reverse animation when set to false
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    public abstract class AnimateBaseProperty<TParent> : BaseAttachedProperty<TParent, Boolean> where 
        TParent : BaseAttachedProperty<TParent, Boolean>, new()
    {
        protected Single DefaultDurationInSeconds => 0.4f;

        public Boolean IsFirstLoad { get; set; } = true;

        /// <summary>
        /// True if this is the very first time the value has been updated
        /// Used to make sure we run the logic at least once during first load
        /// </summary>
		private readonly Dictionary<Object, Boolean> _alreadyLoaded = new Dictionary<Object, Boolean>();

        /// <summary>
        /// The most recent value used if we get a value changed before we do the first load
        /// </summary>
		private readonly Dictionary<Object, Boolean> _firstLoadValue = new Dictionary<Object, Boolean>();

		protected override void OnValueUpdated(DependencyObject sender, Object value)
        {
            if (!(sender is FrameworkElement element))
                return;

            // Try and get the already loaded reference
            //var alreadyLoadedReference = _alreadyLoaded.FirstOrDefault(f => f.Key == sender);
            var alreadyLoadedReference = _alreadyLoaded.FirstOrDefault(f => ReferenceEquals(f.Key, sender));

            // Try and get the first load reference
            //var firstLoadReference = _firstLoadValue.FirstOrDefault(f => f.Key == sender);
            var firstLoadReference = _firstLoadValue.FirstOrDefault(f => ReferenceEquals(f.Key, sender));

            // Don't fire if the value doesn't change
            if ((Boolean)sender.GetValue(ValueProperty) == (Boolean)value && alreadyLoadedReference.Key != null)
                return;

            //the first time prop. run the FrameworkItem isn't loaded, so we haven't got width of item, which we need for animation
            //so we firstly wait for element to load and then do the action. 
            //Otherwise any other time the item is already loaded, and we just call action.
            if (alreadyLoadedReference.Key == null)
            {
                // Create weak reference
                Object obj = sender;

                // Flag that we are in first load but have not finished it
                _alreadyLoaded[obj] = false;

                // Start off hidden before we decide how to animate
                element.Visibility = Visibility.Hidden;

                // Create a single self-unhookable event for the elements Loaded event
				async void OnLoaded(object s, RoutedEventArgs e)
				{
					element.Loaded -= OnLoaded; //so, the function OnLoaded will fire only once

					// Slight delay after load is needed for some elements to get laid out and their width/heights correctly calculated
					await Task.Delay(5);
					// Refresh the first load value in case it changed since the 5ms delay
					//firstLoadReference = _firstLoadValue.FirstOrDefault(f => f.Key == sender);
					firstLoadReference = _firstLoadValue.FirstOrDefault(f => ReferenceEquals(f.Key, sender));
					DoAnimation(element, firstLoadReference.Key != null ? firstLoadReference.Value : (Boolean)value, true);

					// Flag that we have finished first load
					_alreadyLoaded[obj] = true;
				}

				//hook into the loaded event of the element - animation will fire on item loaded
                element.Loaded += OnLoaded;
            }
            // If we have started a first load but not fired the animation yet, update the property
            else if (alreadyLoadedReference.Value == false)
                _firstLoadValue[sender] = (Boolean)value;
            else
                // Do desired animation
                DoAnimation(element, (Boolean)value, false);
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">The new value</param>
        /// <param name="firstLoad">Is first loaded</param>
        protected virtual void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {

        }
    }

    /// <summary>
    /// Animates a framework element sliding it in from the left on show
    /// and sliding out to the left on hide
    /// </summary>
    public sealed class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Left, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Left, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
        }
    }

    public sealed class AnimateSlideInFromLeftMarginProperty : AnimateBaseProperty<AnimateSlideInFromLeftMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Left, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: true);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Left, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: true);
        }
    }

    /// <summary>
    /// Animates a framework element fading in on show
    /// and fading out on hide
    /// </summary>
    public sealed class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.FadeInAsync(firstLoad, firstLoad ? 0 : DefaultDurationInSeconds);
            else
                // Animate out
                await element.FadeOutAsync(firstLoad ? 0 : DefaultDurationInSeconds);
        }
    }

    public class AnimateSlideInFromRightProperty : AnimateBaseProperty<AnimateSlideInFromRightProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Right, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Right, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
        }
    }

    public sealed class AnimateSlideInFromRightMarginProperty : AnimateBaseProperty<AnimateSlideInFromRightMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Right, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: true);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Right, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: true);
        }
    }

    public sealed class AnimateSlideInFromTopProperty : AnimateBaseProperty<AnimateSlideInFromTopProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Top, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Top, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Bottom, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Bottom, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: false);
        }
    }

    public sealed class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            // Animate in
            await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Bottom, !value, !value ? 0 : DefaultDurationInSeconds, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomMarginProperty : AnimateBaseProperty<AnimateSlideInFromBottomMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, Boolean value, Boolean firstLoad)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInAsync(EAnimationSlideInDirection.Bottom, firstLoad, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: true);
            else
                // Animate out
                await element.SlideAndFadeOutAsync(EAnimationSlideInDirection.Bottom, firstLoad ? 0 : DefaultDurationInSeconds, keepMargin: true);
        }
    }
}
