using ClientApp.ViewModels;

namespace ClientApp.Other
{
	internal sealed class CViewModelLocator
    {
        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static CViewModelLocator Instance { get; } = new CViewModelLocator();

        /// <summary>
        /// The application view model
        /// </summary>
        //public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

        private CApplicationViewModel _applicationViewModel;
        public CApplicationViewModel ApplicationViewModel => _applicationViewModel ?? (_applicationViewModel = new CApplicationViewModel());

    }
}
