using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DTO;
using log4net;
using Newtonsoft.Json;
using Other;

namespace ClientApp.ServiceProxies
{
	internal sealed class CChatsServiceProxy : IDisposable
    {
        private readonly HttpClient _client;
        private readonly ILog _logger = SLogger.GetLogger();

        public CChatsServiceProxy()
        {
            _client = new HttpClient();

            String baseUrl = ConfigurationManager.AppSettings["MessengerServiceUrl"];

            Console.WriteLine($@"MessengerService url: {baseUrl}");

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            //or xml, or bsod
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<CChatDto> GetChats(Guid userId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CChatDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Int32 GetUnreadMessagesCount(Guid userId, Guid chatId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats/{chatId}/messages/unread/count"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<Int32>().Result
                    : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public CMessageDto GetLastMessage(Guid userId, Guid chatId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats/{chatId}/messages/last"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<CMessageDto>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public CChatDto GetDialog(Guid userId, Guid participantId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{userId}/chats/{participantId}"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<CChatDto>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Boolean CreateChat(CNewChatDto chatDto)
        {
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    $"api/chats/new",
                    new StringContent(JsonConvert.SerializeObject(chatDto), Encoding.UTF8, "application/json")).Result;

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

        #region IDisposable

        public void Dispose() => _client?.Dispose();

        #endregion
    }
}

