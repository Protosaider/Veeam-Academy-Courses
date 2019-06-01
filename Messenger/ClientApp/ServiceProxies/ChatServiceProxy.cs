using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DTO;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Other;

namespace ClientApp.ServiceProxies
{
	internal sealed class CChatServiceProxy : IDisposable
    {
        private readonly HttpClient _client;
        private readonly ILog _logger = SLogger.GetLogger();

        public CChatServiceProxy()
        {
            _client = new HttpClient();

            String baseUrl = ConfigurationManager.AppSettings["MessengerServiceUrl"];

            Console.WriteLine($@"MessengerService url: {baseUrl}");

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            //or xml, or bsod
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<CMessageDto> GetAllMessages(Guid userId, Guid chatId, Int32 limit, Int32 offset)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats/{chatId}/messages?limit={limit}&offset={offset}"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CMessageDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IReadOnlyCollection<CMessageDto> GetNewMessages(Guid userId, Guid chatId, DateTimeOffset lastRequestDate, Int64 lastUsn, Int32 limit, Int32 offset)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats/{chatId}/messages?lastRequestDate={System.Net.WebUtility.UrlEncode(lastRequestDate.ToString(System.Globalization.CultureInfo.InvariantCulture))}&lastUsn={lastUsn}&limit={limit}&offset={offset}"
                    //$"api/{userId}/chats/{chatId}/messages?lastRequestDate={JsonConvert.SerializeObject(lastRequestDate.ToUniversalTime(), Formatting.Indented, new IsoDateTimeConverter())}&limit={limit}&offset={offset}"
                    //$"api/{userId}/chats/{chatId}/messages?lastRequestDateTime={lastRequestDate.LocalDateTime.ToString(System.Globalization.CultureInfo.InvariantCulture)}&lastRequestOffset={lastRequestDate.Offset}&limit={limit}&offset={offset}"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<List<CMessageDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //[ActionName("sendmsg")]
        public CMessagePostedDto SendMessage(CNewMessageDto message)
        {
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    $"api/chats/messages/new",
                    new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json")
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<CMessagePostedDto>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Boolean ReadMessages(CReadMessagesDto readMessagesDto)
        {
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    $"api/chats/messages/read",
                    new StringContent(JsonConvert.SerializeObject(readMessagesDto), Encoding.UTF8, "application/json")
                ).Result;

                return response.IsSuccessStatusCode
                    ? response.Content.ReadAsAsync<Boolean>().Result
                    : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<CUserDto> GetChatParticipants(Guid userId, Guid chatId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats/{chatId}/participants"
                ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CUserDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region IDisposable

        public void Dispose() => _client?.Dispose();

        #endregion

    }
}

