using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.DataSuppliers.Data
{
	internal class CUserData
    {
        public String Login { get; }

        public CUserData(String login)
        {
            Login = login;
        }
    }
}
