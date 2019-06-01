using ClientApp.DataSuppliers.Data;
using ClientApp.ViewModels.Base;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientApp.Other;

namespace ClientApp.ViewModels.ChatPage
{
	public sealed class ChatCreateListItemViewModel : BaseViewModel
    {
        private Guid _id;
        private String _name;
        private String _initials;
        private String _profilePictureRgb;

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
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

        public String ProfilePictureRgb
        {
            get => _profilePictureRgb;
            set
            {
                _profilePictureRgb = value;
                OnPropertyChanged();
            }
        }

        public Guid ContactId => _id;
        private Boolean _isSelected;
        public Boolean IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                OnSelection?.Invoke(_id, _isSelected);
            }
        }

        public Action<Guid, Boolean> OnSelection;


        public ICommand UncheckCommand { get; }
        public ChatCreateListItemViewModel(CContactData contactData)
        {
            _id = contactData.UserId;
            Name = contactData.Login;
            Initials = Name.Substring(0, 2);
            ProfilePictureRgb = Name.FromLogin().ToColorCode();
            UncheckCommand = new CRelayCommand((obj) => IsSelected ^= true);
        }
    }
}
