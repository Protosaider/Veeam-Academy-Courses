using System;
using System.Threading.Tasks;
using DTO;

namespace ClientApp.DataSuppliers
{
    public interface IAuthSupplier
    {
        CTokenDto LogIn(String login, String password);
        Task<CTokenDto> LogInAsync(String login, String password);
        String SignUp(String login, String password, String avatar);
    }
}