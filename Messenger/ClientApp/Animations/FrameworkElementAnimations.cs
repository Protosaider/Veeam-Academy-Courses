using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ClientApp.Animations
{
	internal static class FrameworkElementAnimations
    {
        public static async Task SlideAndFadeInAsync(this FrameworkElement element, EAnimationSlideInDirection direction, Boolean isFirstLoad, Double durationInSeconds = 0.3f, Boolean keepMargin = true, Int32 size = 0)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Slide in the correct direction
            switch (direction)
            {
                // Add slide from left animation
                case EAnimationSlideInDirection.Left:
                    sb.AddSlideFromLeft(durationInSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide from right animation
                case EAnimationSlideInDirection.Right:
                    sb.AddSlideFromRight(durationInSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide from top animation
                case EAnimationSlideInDirection.Top:
                    sb.AddSlideFromTop(durationInSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                // Add slide from bottom animation
                case EAnimationSlideInDirection.Bottom:
                    sb.AddSlideFromBottom(durationInSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
            // Add fade in animation
            sb.AddFadeIn(durationInSeconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating or its the first load
			Double tolerance = Double.Epsilon * 1.0E5;

			if (Math.Abs(durationInSeconds) > tolerance || isFirstLoad)
                element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((Int32)(durationInSeconds * 1000));
        }

        public static async Task SlideAndFadeOutAsync(this FrameworkElement element, EAnimationSlideInDirection direction, Double durationInSeconds = 0.3f, Boolean keepMargin = true, Int32 size = 0)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Slide in the correct direction
            switch (direction)
            {
                // Add slide to left animation
                case EAnimationSlideInDirection.Left:
                    sb.AddSlideToLeft(durationInSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide to right animation
                case EAnimationSlideInDirection.Right:
                    sb.AddSlideToRight(durationInSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide to top animation
                case EAnimationSlideInDirection.Top:
                    sb.AddSlideToTop(durationInSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                // Add slide to bottom animation
                case EAnimationSlideInDirection.Bottom:
                    sb.AddSlideToBottom(durationInSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}

            // Add fade in animation
            sb.AddFadeOut(durationInSeconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
			Double tolerance = Double.Epsilon * 1.0E5;
            if (Math.Abs(durationInSeconds) > tolerance)
                element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((Int32)(durationInSeconds * 1000));

            // Make element invisible
            element.Visibility = Visibility.Hidden;
        }

        public static async Task FadeInAsync(this FrameworkElement element, Boolean isFirstLoad, Double durationInSeconds = 0.3f)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add fade in animation
            sb.AddFadeIn(durationInSeconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating or its the first load
			Double tolerance = Double.Epsilon * 1.0E5;
            if (Math.Abs(durationInSeconds) > tolerance || isFirstLoad)
                element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((Int32)(durationInSeconds * 1000));
        }


        public static async Task FadeOutAsync(this FrameworkElement element, Double isFirstLoad = 0.3f)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add fade in animation
            sb.AddFadeOut(isFirstLoad);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating or its the first load
			Double tolerance = Double.Epsilon * 1.0E5;
            if (Math.Abs(isFirstLoad) > tolerance)
                element.Visibility = Visibility.Visible;

            // Wait for it to finish
            await Task.Delay((Int32)(isFirstLoad * 1000));

            // Fully hide the element
            element.Visibility = Visibility.Collapsed;
        }

    }
}
