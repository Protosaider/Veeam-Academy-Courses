using System.Security;

namespace ClientApp.Other
{
    /// An interface for a class that can provide a secure password - for not to use attached property
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
