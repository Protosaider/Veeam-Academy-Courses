using System;

namespace ClientApp.DataSuppliers.Data
{
	internal sealed class CParticipantData
    {
        public String Login { get; }
        public Int32 ActivityStatus { get; }

        public CParticipantData(String login, Int32 activityStatus)
        {
            Login = login;
            ActivityStatus = activityStatus;
        }
    }
}