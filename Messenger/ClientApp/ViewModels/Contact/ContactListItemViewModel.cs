using System;
using System.Windows.Input;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ViewModels.Base;
using DTO;

namespace ClientApp.ViewModels.Contact
{
    public sealed class ContactListItemViewModel : BaseViewModel
    {
        private String _name;
        private DateTimeOffset _lastActiveTime;
        private Int32 _activityStatus;
        private String _initials;
        private String _profilePictureRgb;
        private Boolean _isSelected;

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTimeOffset LastActiveTime
        {
            get => _lastActiveTime;
            set
            {
                _lastActiveTime = value;
                OnPropertyChanged();
            }
        }

        public String Initials
        {
            get => _initials;
            set
            {
                _initials = value;
                OnPropertyChanged();
            }
        }

        public Int32 ActivityStatus
        {
            get => _activityStatus;
            set
            {
                _activityStatus = value; 
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The RGB values (in hex) for the background color of the profile picture
        /// For example FF00FF for Red and Blue mixed
        /// </summary>
        public String ProfilePictureRgb
        {
            get => _profilePictureRgb;
            set
            {
                _profilePictureRgb = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public Guid Id { get; }

        private Boolean _isChatOpened;
        public Boolean IsChatOpened
        {
            get => _isChatOpened;
            set
            {
                _isChatOpened = value;
                OnPropertyChanged();
            }
        }

        public ContactListItemViewModel(CContactData contactData)
        {
            Id = contactData.Id;
            Name = contactData.Login;
            Initials = Name.Substring(0, 2);
            LastActiveTime = contactData.LastActiveDate;
            ActivityStatus = contactData.ActivityStatus;
            ProfilePictureRgb = Name.FromLogin().ToColorCode();
        }

        public ContactListItemViewModel() { }

        private COpenChatCommand _openChatCommandClass;       
        public COpenChatCommand OpenChatCommandClass => _openChatCommandClass ?? (_openChatCommandClass = new COpenChatCommand(() => IsChatOpened = true, () => !IsChatOpened));
        private ICommand _openChatCommand;
        public ICommand OpenChatCommand => _openChatCommand ?? (_openChatCommand = OpenChatCommandClass);

    }
}