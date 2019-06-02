using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ClientApp.Other;
using DTO;
using log4net;
using Newtonsoft.Json;

namespace ClientApp.ServiceProxies
{
	internal sealed class CAuthServiceProxy : IDisposable
    {
        private readonly HttpClient _client;
        private readonly ILog _logger = SLogger.GetLogger();

        public CAuthServiceProxy()
        {
            _client = new HttpClient();

            String baseUrl = ConfigurationManager.AppSettings["MessengerServiceUrl"];

            Console.WriteLine($@"Setting up parameters of HttpClient in {nameof(CAuthServiceProxy)}");
            _logger.LogInfo($"Setting up parameters of HttpClient in {nameof(CAuthServiceProxy)}");

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            //or xml, or bsod
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public CTokenDto LogIn(CCredentialsDto credentials)
        {
            Console.WriteLine($@"Service method '{nameof(LogIn)}({credentials})' is called");
            _logger.LogInfo($"Service method '{nameof(LogIn)}({credentials})' is called");
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/auth/login?login={credentials.Login}&password={credentials.Password}"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<CTokenDto>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
				_logger.LogError(@"MessengerService Error", e);
                throw;
            }
        }

        public String SignUp(CSignUpDto signUpData)
        {
            Console.WriteLine($@"Service method '{nameof(SignUp)}({signUpData})' is called");
            _logger.LogInfo($"Service method '{nameof(SignUp)}({signUpData})' is called");
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    $"api/auth/signup",
                    new StringContent(JsonConvert.SerializeObject(signUpData), Encoding.UTF8, "application/json")
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsStringAsync().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
				_logger.LogError(@"MessengerService Error", e);
                throw;
            }
        }

        #region IDisposable

        public void Dispose()
        {
            _client?.Dispose();
        }

        #endregion
    }
}