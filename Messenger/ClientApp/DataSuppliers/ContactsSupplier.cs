using ClientApp.ServiceProxies;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;

namespace ClientApp.DataSuppliers
{
	internal sealed class CContactsSupplier : IContactsSupplier
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

			return _service.GetContacts(ownerId).Select(contactDto => new CContactData(contactDto.Id, contactDto.OwnerId, contactDto.UserId, contactDto.IsBlocked, contactDto.UserData.Login, contactDto.UserData.LastActiveTime, contactDto.UserData.ActivityStatus)).ToList();
        }

        public IReadOnlyCollection<Guid> GetHasDialogContactsId(Guid ownerId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetHasDialogContactsId)}({ownerId})' is called");

			return _service.GetHasDialogContacts(ownerId).Select(contactDto => contactDto.UserId).ToList();
        }

        public IReadOnlyCollection<CContactData> FindContacts(Guid ownerId, String lastSearchText)
        {
            _logger.LogInfo($"Supplier method '{nameof(FindContacts)}({ownerId}, {lastSearchText})' is called");

			return _service.SearchNewContacts(ownerId, lastSearchText).Select(contactDto => new CContactData(contactDto.Id, contactDto.OwnerId, contactDto.UserId, contactDto.IsBlocked, contactDto.UserData.Login, contactDto.UserData.LastActiveTime, contactDto.UserData.ActivityStatus)).ToList();
        }

        public IReadOnlyDictionary<Guid, DateTimeOffset> GetContactsLastActiveDate(Guid ownerId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetContactsLastActiveDate)}({ownerId})' is called");

			return _service.GetContactsLastActivityDate(ownerId).ToDictionary(lastActiveDate => lastActiveDate.UserId, lastActiveDate => lastActiveDate.LastActiveDate);
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
