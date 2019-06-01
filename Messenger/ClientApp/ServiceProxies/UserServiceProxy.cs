using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using DTO;
using log4net;
using Newtonsoft.Json;
using Other;

namespace ClientApp.ServiceProxies
{
	internal sealed class CUserServiceProxy : IDisposable
    {
        private readonly HttpClient _client;

        private readonly ILog _logger = SLogger.GetLogger();

        public CUserServiceProxy()
        {
            _client = new HttpClient();

            String baseUrl = ConfigurationManager.AppSettings["MessengerServiceUrl"];
            Console.WriteLine($@"MessengerService url: {baseUrl}");

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            //TODO Здесь, как я понимаю, мы указываем тип, в который должны данные сериализоваться и передаваться
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //application/x-www-form-urlencoded   application/http   text/html   image/png
        }

        public Boolean UpdateActivityStatus(CActivityStatusDto activityStatus)
        {
            Console.WriteLine($@"Service method '{nameof(UpdateActivityStatus)}({activityStatus})' is called");
            _logger.LogInfo($"Service method '{nameof(UpdateActivityStatus)}({activityStatus})' is called");
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    $"api/user/activityStatus",
                    new StringContent(JsonConvert.SerializeObject(activityStatus), Encoding.UTF8, "application/json")
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<Boolean>().Result
                    : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Boolean UpdateLastActiveDate(CLastActiveDateDto lastActiveDate)
        {
            Console.WriteLine($@"Service method '{nameof(UpdateLastActiveDate)}({lastActiveDate})' is called");
            _logger.LogInfo($"Service method '{nameof(UpdateLastActiveDate)}({lastActiveDate})' is called");
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    //$"api/user/{userId}/lastActive?lastActiveDate={currentDate}"
                    //$"api/user/{userId}/lastActive?lastActiveDate={System.Net.WebUtility.UrlEncode(currentDate.ToString(System.Globalization.CultureInfo.InvariantCulture))}"
                    $"api/user/lastActiveDate",
                    new StringContent(JsonConvert.SerializeObject(lastActiveDate), Encoding.UTF8, "application/json")
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<Boolean>().Result
                    : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public CUserDto GetUserData(Guid userId)
        {
            Console.WriteLine($@"Service method '{nameof(GetUserData)}({userId})' is called");
            _logger.LogInfo($"Service method '{nameof(GetUserData)}({userId})' is called");
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/user/{userId}/data"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<CUserDto>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region IDisposable
        public void Dispose() => _client.Dispose();
        #endregion

    }
}
