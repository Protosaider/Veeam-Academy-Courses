﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using ClientApp.Other;
using DTO;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientApp.ServiceProxies
{
	internal sealed class CContactsServiceProxy : IDisposable
    {
        private readonly HttpClient _client;
		private readonly ILog _logger = SLogger.GetLogger();

        public CContactsServiceProxy()
        {
            _client = new HttpClient();

            String baseUrl = ConfigurationManager.AppSettings["MessengerServiceUrl"];

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<CContactDto> GetContacts(Guid ownerId)
        {
            _logger.LogInfo($"Service method '{nameof(GetContacts)}({ownerId})' is called");
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{ownerId}/contacts"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CContactDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
				_logger.LogError(@"MessengerService Error", e);
                throw;
            }
        }

        public IEnumerable<CContactDto> GetHasDialogContacts(Guid ownerId)
        {
            _logger.LogInfo($"Service method '{nameof(GetHasDialogContacts)}({ownerId})' is called");
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{ownerId}/contacts/hasDialog"
                ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CContactDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
				_logger.LogError(@"MessengerService Error", e);
                throw;
            }
        }

        public IEnumerable<CContactDto> SearchNewContacts(Guid ownerId, String q)
        {
            _logger.LogInfo($"Service method '{nameof(SearchNewContacts)}({ownerId}, {q})' is called");
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{ownerId}/contacts?q={q}"
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CContactDto>>().Result
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
				_logger.LogError(@"MessengerService Error", e);
                throw;
            }          
        }

        //TODO: Check if not existed => create new contact
        public String PostContact(JObject dataToPost)
        {
            Console.WriteLine($@"Service method '{nameof(PostContact)}({dataToPost})' is called");
			_logger.LogInfo($"Service method '{nameof(PostContact)}({dataToPost})' is called");

            try
            {
                //var content = new HttpRequestMessage();
                //content.Headers.Add();
                //content.Content = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("OSVersion", deviceInformation.OperatingSystem),
                //    new KeyValuePair<string, string>("DeviceModel", deviceInformation.FriendlyName),
                //});


                HttpResponseMessage response = _client.PostAsync("api/contacts/add",
                    //new StringContent(JsonConvert.SerializeObject(ownerId), Encoding.UTF8, "application/json")
                    new StringContent(JsonConvert.SerializeObject(dataToPost), Encoding.UTF8, "application/json")
                    ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsStringAsync().Result
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"MessengerService Error");
				_logger.LogError(@"MessengerService Error", ex);
                throw;
            }

        }

        public String DeleteContact(JObject dataToDelete)
        {
            Console.WriteLine($@"Service method '{nameof(DeleteContact)}({dataToDelete})' is called");
			_logger.LogInfo($"Service method '{nameof(DeleteContact)}({dataToDelete})' is called");

            try
            {
                HttpResponseMessage response = _client.PostAsync("api/contacts/delete",
                    new StringContent(JsonConvert.SerializeObject(dataToDelete), Encoding.UTF8, "application/json")
                ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsStringAsync().Result
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"MessengerService Error");
				_logger.LogError(@"MessengerService Error", ex);
                throw;
            }
        }

        public IEnumerable<CLastActiveDateDto> GetContactsLastActivityDate(Guid ownerId)
        {
            _logger.LogInfo($"Service method '{nameof(GetContactsLastActivityDate)}({ownerId})' is called");
            try
            {
                HttpResponseMessage response = _client.GetAsync(
                    $"api/{ownerId}/contacts/lastActiveDate"
                ).Result;

                return (response.IsSuccessStatusCode)
                    ? response.Content.ReadAsAsync<IEnumerable<CLastActiveDateDto>>().Result
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

        public void Dispose() => _client.Dispose();

        #endregion
    }
}

