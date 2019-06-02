using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ClientApp.Animations
{
	internal static class StoryboardHelpers
    {
        #region Slide from/to left
        /// <summary>
        /// Adds a slide from left animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="durationInSeconds">The time the animation will take</param>
        /// <param name="offset">The distance to the left to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        public static void AddSlideFromLeft(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9, Boolean keepMargin = true)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio,
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }

        public static void AddSlideToLeft(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9, Boolean keepMargin = true)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
                DecelerationRatio = decelerationRatio,
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }


        #endregion

        #region Slide from/to right


        public static void AddSlideFromRight(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9, Boolean keepMargin = true)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio,
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }

        public static void AddSlideToRight(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9, Boolean keepMargin = true)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(0),
                To = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                DecelerationRatio = decelerationRatio,
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }


        #endregion

        #region Slide from/to top

        public static void AddSlideFromTop(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9f, Boolean keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(0, -offset, 0, keepMargin ? offset : 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        public static void AddSlideToTop(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9f, Boolean keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(0),
                To = new Thickness(0, -offset, 0, keepMargin ? offset : 0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }


        #endregion

        #region Slide from/to bottom


        public static void AddSlideFromBottom(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9f, Boolean keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        public static void AddSlideToBottom(this Storyboard storyboard, Double durationInSeconds, Double offset, Double decelerationRatio = 0.9f, Boolean keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = new Thickness(0),
                To = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }


        #endregion

        #region Fade in/out
        public static void AddFadeIn(this Storyboard storyboard, Double durationInSeconds, Boolean hasFromValue = false)
        {
            var fadeInAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                To = 1.0,
            };

            // Animate from if requested
            if (hasFromValue)
                fadeInAnimation.From = 0.0;

            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(fadeInAnimation);
        }

        public static void AddFadeOut(this Storyboard storyboard, Double durationInSeconds)
        {
            var fadeInAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds)),
                From = 1.0,
                To = 0.0,
            };
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(fadeInAnimation);
        } 
        #endregion
    }
}
