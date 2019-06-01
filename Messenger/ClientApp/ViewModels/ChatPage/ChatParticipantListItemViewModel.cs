using System;
using ClientApp.DataSuppliers.Data;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ChatPage
{
    public class ChatParticipantListItemViewModel : BaseViewModel
    {
        private String _name;
        private Int32 _activityStatus;
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

        public Int32 ActivityStatus
        {
            get => _activityStatus;
            set
            {
                _activityStatus = value;
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

        public ChatParticipantListItemViewModel(CParticipantData participantData)
        {
            Name = participantData.Login;
            Initials = Name.Substring(0, 2);
            ActivityStatus = participantData.ActivityStatus;
            //ProfilePictureRgb = Name.GetHashCode() % 10;
        }
    }
}