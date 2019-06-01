using ClientApp.Other;
using System;
using System.Windows.Input;
using ClientApp.ViewModels.Base;
using ClientApp.ViewModels.ChatPage;
using ClientApp.ViewModels.Contact;
using ClientApp.Activities;

namespace ClientApp.ViewModels
{
	internal sealed class CApplicationViewModel : BaseViewModel, IDisposable
    {
        private Boolean _isSideMenuVisible;
        private SideMenuContent _currentSideMenuContent = SideMenuContent.None;
        private BaseViewModel _currentSideMenuViewModel;

        private EApplicationPage _currentPage = EApplicationPage.LogIn;
        private BaseViewModel _currentPageViewModel;

        public Boolean IsSideMenuVisible
        {
            get => _isSideMenuVisible;
            set
            {
                if (_isSideMenuVisible == value)
                    return;
                _isSideMenuVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The view model to use for the current page when the CurrentSideMenu changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
		private BaseViewModel CurrentSideMenuViewModel
        {
            get => _currentSideMenuViewModel;
            set
            {
                _currentSideMenuViewModel = value;
                OnPropertyChanged();
            }
        }


        public SideMenuContent CurrentSideMenuContent
        {
            get => _currentSideMenuContent;
            private set
            {
                _currentSideMenuContent = value;
                OnPropertyChanged();
            }
        }


        public EApplicationPage CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged();
            }
        }


		private ICommand OpenChatCommand { get; }
		private ICommand OpenContactsCommand { get; }

		private void OpenChat() => GoToControl(SideMenuContent.Chats, new ChatListViewModel());
		private void OpenContacts() => GoToControl(SideMenuContent.Contacts, new ContactListViewModel());


        public void GoToPage(EApplicationPage page, BaseViewModel viewModel = null)
        {
            // Set the view model
            CurrentPageViewModel = viewModel;

            // See if page has changed
            var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));

            // Show side menu or not?
            IsSideMenuVisible = page == EApplicationPage.Chat;
        }

        public void GoToControl(SideMenuContent control, BaseViewModel viewModel = null)
        {
            CurrentSideMenuViewModel = viewModel;

            var different = CurrentSideMenuContent != control;

            CurrentSideMenuContent = control;

            if (!different)
                OnPropertyChanged(nameof(CurrentSideMenuContent));
        }


        public CApplicationViewModel()
        {
            OpenChatCommand = new CRelayCommand(x => OpenChat(), y => true);
            OpenContactsCommand = new CRelayCommand(x => OpenContacts(), y => true);            
        }

        private readonly CUpdateLastActiveDateActivity _updateLastActiveDateActivity = new CUpdateLastActiveDateActivity();
        public void RunUpdateLastActiveDateActivity() => _updateLastActiveDateActivity.Run();
        public void CancelUpdateLastActiveDateActivity() => _updateLastActiveDateActivity.Cancel();

        public void Dispose()
        {
            _updateLastActiveDateActivity.Cancel();
        }
    }
}
