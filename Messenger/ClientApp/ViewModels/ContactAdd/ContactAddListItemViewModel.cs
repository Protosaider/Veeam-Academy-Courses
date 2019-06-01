using System;
using System.Windows.Input;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ContactAdd
{
    public class ContactAddListItemViewModel : BaseViewModel
    {
        private readonly Guid _id;
        private String _name;
        private String _lastActiveTime;
        private String _initials;
        private String _profilePictureRgb;
        private Boolean _isSelected;

        public Guid Id => _id;

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public String LastActiveTime
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

        public ContactAddListItemViewModel(CContactData contactData)
        {
            _id = contactData.UserId;
            Name = contactData.Login;
            Initials = Name.Substring(0, 2);
            ProfilePictureRgb = Name.FromLogin().ToColorCode();
        }

        public ContactAddListItemViewModel() { }

        private AddContactCommand _addContactCommandClass;
        public AddContactCommand AddContactCommandClass => _addContactCommandClass ?? (_addContactCommandClass = new AddContactCommand());
        private ICommand _addContactCommand;
        public ICommand AddContactCommand => _addContactCommand ?? (_addContactCommand = AddContactCommandClass);

    }
}