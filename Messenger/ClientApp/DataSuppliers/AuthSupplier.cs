using System;
using System.Threading.Tasks;
using ClientApp.ServiceProxies;
using DTO;
using log4net;
using Other;

namespace ClientApp.DataSuppliers
{
	internal sealed class CAuthSupplier : IAuthSupplier, IDisposable
    {
        private readonly CAuthServiceProxy _service;
        private readonly ILog _logger = SLogger.GetLogger();

		private CAuthSupplier()
        {
            _service = new CAuthServiceProxy();
        }

        internal static CAuthSupplier Create()
        {
            try
            {
                return new CAuthSupplier();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region IDisposable

        public void Dispose()
        {
            _service?.Dispose();
        }

        #endregion

        #region IAuthSupplier

        public CTokenDto LogIn(String login, String password)
        {
            Console.WriteLine($@"Supplier method '{nameof(LogIn)}({login})' is called");
            _logger.LogInfo($"Supplier method '{nameof(LogIn)}({login})' is called");

            CCredentialsDto credentials = new CCredentialsDto(
                login,
                password
                );
            Console.WriteLine($@"Credentials = {credentials}");
            _logger.LogInfo($"Credentials = {credentials}");
            return _service.LogIn(credentials);
        }

        public Task<CTokenDto> LogInAsync(String login, String password)
        {
            Console.WriteLine($@"Supplier method '{nameof(LogInAsync)}({login})' is called");
            _logger.LogInfo($"Supplier method '{nameof(LogInAsync)}({login}, {password})' is called");

            CCredentialsDto credentials = new CCredentialsDto(
                login, 
                password
                );

            Console.WriteLine($@"Credentials = {credentials}");
            _logger.LogInfo($"Credentials = {credentials}");

            return Task.Run<CTokenDto>(() => _service.LogIn(credentials));
        }


        public String SignUp(String login, String password, String avatar)
        {
            Console.WriteLine($@"Supplier method '{nameof(SignUp)}({login}, {avatar})' is called");
            _logger.LogInfo($"Supplier method '{nameof(SignUp)}({login}, {avatar})' is called");

            CSignUpDto signUpData = new CSignUpDto(
                new CCredentialsDto(
                    login, 
                    password
                    ),
                avatar
            );

            Console.WriteLine($@"Data to sign up = {signUpData}");
            _logger.LogInfo($"Data to sign up = {signUpData}");

            return _service.SignUp(signUpData);
        }

        #endregion

    }
}