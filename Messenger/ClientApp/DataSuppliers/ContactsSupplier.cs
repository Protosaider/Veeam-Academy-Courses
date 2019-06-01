using ClientApp.Models.DataSuppliers;
using ClientApp.ServiceProxies;
using ClientApp.ViewModels.Contact;
using DTO;
using log4net;
using Newtonsoft.Json.Linq;
using Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.ViewModels.ChatPage;
using ClientApp.DataSuppliers.Data;

namespace ClientApp.DataSuppliers
{
    public sealed class CContactsSupplier : IContactsSupplier
    {
        private readonly CContactsServiceProxy _service;
        private readonly ILog _logger = SLogger.GetLogger();

		private CContactsSupplier()
        {
            _service = new CContactsServiceProxy();
        }

		internal static CContactsSupplier Create()
        {
            try
            {
                return new CContactsSupplier();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IReadOnlyCollection<CContactData> GetContacts(Guid ownerId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetContacts)}({ownerId})' is called");
            List<CContactData> contacts = new List<CContactData>();

            foreach (var contactDto in _service.GetContacts(ownerId))
            {
                var contact = new CContactData(contactDto.Id, contactDto.OwnerId, contactDto.UserId, contactDto.IsBlocked, contactDto.UserData.Login, contactDto.UserData.LastActiveTime, contactDto.UserData.ActivityStatus);
                contacts.Add(contact);
            }

            return contacts;
        }

        public IReadOnlyCollection<Guid> GetHasDialogContactsId(Guid ownerId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetHasDialogContactsId)}({ownerId})' is called");
            List<Guid> contacts = new List<Guid>();
            foreach (var contactDto in _service.GetHasDialogContacts(ownerId))
            {
                var contactId = contactDto.UserId;
                contacts.Add(contactId);
            }

            return contacts;
        }

        public IReadOnlyCollection<CContactData> FindContacts(Guid ownerId, String lastSearchText)
        {
            _logger.LogInfo($"Supplier method '{nameof(FindContacts)}({ownerId}, {lastSearchText})' is called");

            List<CContactData> contacts = new List<CContactData>();
            foreach (var contactDto in _service.SearchNewContacts(ownerId, lastSearchText))
            {
                contacts.Add(new CContactData(contactDto.Id, contactDto.OwnerId, contactDto.UserId, contactDto.IsBlocked, contactDto.UserData.Login, contactDto.UserData.LastActiveTime, contactDto.UserData.ActivityStatus));
            }

            return contacts;
        }

        public IReadOnlyDictionary<Guid, DateTimeOffset> GetContactsLastActiveDate(Guid ownerId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetContactsLastActiveDate)}({ownerId})' is called");

            Dictionary<Guid, DateTimeOffset> lastActiveDates = new Dictionary<Guid, DateTimeOffset>();
            foreach (var lastActiveDate in _service.GetContactsLastActivityDate(ownerId))
            {
                lastActiveDates.Add(lastActiveDate.UserId, lastActiveDate.LastActiveDate);
            }

            return lastActiveDates;
        }


        #region IContactSupplier

        public Task<String> AddContact(Guid ownerId, Guid userId)
        {
            _logger.LogInfo($"Supplier method '{nameof(AddContact)}({ownerId}, {userId})' is called");

            return Task.Run<String>(() =>
            {
                var dataToPost = new JObject();
                dataToPost.Add("OwnerId", ownerId);
                dataToPost.Add("UserId", userId);
                return _service.PostContact(dataToPost);
            });
        }

        public String DeleteContact(Guid ownerId, Guid userId)
        {
            _logger.LogInfo($"Supplier method '{nameof(DeleteContact)}({ownerId}, {userId})' is called");
            var dataToPost = new JObject();
            dataToPost.Add("OwnerId", ownerId);
            dataToPost.Add("UserId", userId);

            return _service.DeleteContact(dataToPost);
        }

        #endregion
    }
}
