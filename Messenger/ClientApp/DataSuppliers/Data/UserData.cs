using System;

namespace ClientApp.DataSuppliers.Data
{
	internal sealed class CUserData
    {
        public String Login { get; }

        public CUserData(String login)
        {
            Login = login;
        }
    }
}
