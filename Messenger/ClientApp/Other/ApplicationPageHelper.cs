using System;
using System.Diagnostics;
using ClientApp.Pages;
using ClientApp.ViewModels.ChatPage;
using ClientApp.ViewModels.LogInPage;
using ClientApp.ViewModels.SignUpPage;

namespace ClientApp.Other
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
	internal static class SApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this EApplicationPage page, Object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case EApplicationPage.LogIn:
                    return new LogInPage(viewModel as CLogInViewModel);

                case EApplicationPage.SignUp:
                    return new SignUpPage(viewModel as CSignUpViewModel);

                case EApplicationPage.Chat:
                    return new ChatPage(viewModel as ChatViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static EApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is LogInPage)
                return EApplicationPage.LogIn;

            if (page is SignUpPage)
                return EApplicationPage.SignUp;

            if (page is ChatPage)
                return EApplicationPage.Chat;

            // Alert developer of issue
            Debugger.Break();
            return default(EApplicationPage);
        }
    }
}
