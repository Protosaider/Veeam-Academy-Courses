using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Other
{
    /// An interface for a class that can provide a secure password - for not to use attached property
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
