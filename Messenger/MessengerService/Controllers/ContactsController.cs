using DataStorage.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Info;
using Newtonsoft.Json.Linq;
using log4net;
using DTO;
using System.Web.Http.Description;
using Common.ServiceLocator;
using DataStorage;
using MessengerService.Other;

namespace MessengerService.Controllers
{
    public sealed class ContactsController : ApiController
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private readonly ICContactInfoDataProvider _contactDataProvider;
        private readonly ICUserInfoDataProvider _userDataProvider;

        public ContactsController()
        {
            var container = SServiceLocator.CreateContainer();
            ConfigureContainer(ref container);
            _contactDataProvider = container.Resolve<ICContactInfoDataProvider>();
            _userDataProvider = container.Resolve<ICUserInfoDataProvider>();
        }

        private static void ConfigureContainer(ref CContainer container)
        {
            container.Register<ICContactInfoDataProvider, CContactInfoDataProvider>(ELifeCycle.Transient);
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<CDataStorageSettings>(ELifeCycle.Transient);
        }

        [Route("api/{ownerId}/contacts")]
        [ResponseType(typeof(IEnumerable<CContactDto>))]
        public IHttpActionResult GetContacts([FromUri]Guid ownerId)
        {
            if (ownerId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(ownerId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new ArgumentNullException(nameof(ownerId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var contactInfos = _contactDataProvider.GetAllContactsByOwnerId(ownerId);

            if (contactInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new Exception("Failed to get all contacts"));
                return NotFound();
            }

            var userInfos = new List<CUserInfo>(contactInfos.Count);

            foreach (var contact in contactInfos)
            {
                var userInfo = _userDataProvider.GetUserData(contact.UserId);
                userInfos.Add(userInfo);
            }

            return Ok(contactInfos.Select((x, i) => new CContactDto(x.Id, x.OwnerId, x.UserId, x.IsBlocked, new CUserDto(userInfos[i].Login, userInfos[i].LastActiveDate, userInfos[i].ActivityStatus))));
        }

        //[Route("api/{ownerId}/contacts?q={q}")]
        //[Route("api/{ownerId}/contacts{q}")]
        [Route("api/{ownerId}/contacts")]
        [ResponseType(typeof(IEnumerable<CContactDto>))]
        public IHttpActionResult GetSearchNewContacts([FromUri]Guid ownerId, [FromUri]String q)
        {
            Boolean hasError = false;

            if (String.IsNullOrEmpty(q))
            {
                ModelState.AddModelError($"{nameof(q)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({q})", new ArgumentNullException(nameof(q), "Incoming data is null"));
                hasError = true;
            }
            else if (ownerId == default(Guid))
            {
                ModelState.AddModelError($"{nameof(ownerId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({q})", new ArgumentNullException(nameof(ownerId), "Incoming data is null"));
                hasError = true;
            }

            if (hasError)
                return BadRequest(ModelState);

            var contactInfos = _userDataProvider.SearchContacts(ownerId, q);

            if (contactInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new Exception("Search of new contacts was failed "));
                return NotFound();
            }

            return Ok(contactInfos.Select((x) => new CContactDto(default(Guid), default(Guid), x.Id, default(Boolean), new CUserDto(x.Login, x.LastActiveDate, x.ActivityStatus))));
        }

        [Route("api/contacts/add")]
        [ResponseType(typeof(String))]
        public IHttpActionResult PostCreateContact([FromBody]JObject data)
        {
            var ownerId = data["OwnerId"].ToObject<Guid>();
            var userId = data["UserId"].ToObject<Guid>();

            var result = _contactDataProvider.CreateContact(ownerId, userId);
            if (result == 0)
                return InternalServerError();
            result = _contactDataProvider.CreateContact(userId, ownerId);
            if (result == 0)
                return InternalServerError();

            return Ok("Contact created");
        }

        [Route("api/contacts/delete")]
        [ResponseType(typeof(String))]
        public IHttpActionResult DeleteContact([FromBody]JObject data)
        {
            var ownerId = data["OwnerId"].ToObject<Guid>();
            var userId = data["UserId"].ToObject<Guid>();

            var result = _contactDataProvider.DeleteContact(ownerId, userId);
            if (result == 0)
                return InternalServerError();
            result = _contactDataProvider.DeleteContact(userId, ownerId);
            if (result == 0)
                return InternalServerError();

            return Ok("Contact deleted");
        }

        [Route("api/{ownerId}/contacts/hasDialog")]
        [ResponseType(typeof(IEnumerable<CContactDto>))]
        public IHttpActionResult GetHasDialogContacts([FromUri]Guid ownerId)
        {
            if (ownerId == default(Guid))
            {
                ModelState.AddModelError($"{nameof(ownerId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new ArgumentNullException(nameof(ownerId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var contactInfos = _contactDataProvider.GetHasDialogContacts(ownerId);

            if (contactInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new Exception("Failed to get contacts without dialogs"));
                return NotFound();
            }

            var userInfos = new List<CUserInfo>(contactInfos.Count);

            foreach (var contact in contactInfos)
            {
                var userInfo = _userDataProvider.GetUserData(contact.UserId);
                userInfos.Add(userInfo);
            }

            return Ok(contactInfos.Select((x, i) => new CContactDto(x.Id, x.OwnerId, x.UserId, x.IsBlocked, new CUserDto(userInfos[i].Login, userInfos[i].LastActiveDate, userInfos[i].ActivityStatus))));
        }

        [Route("api/{ownerId}/contacts/lastActiveDate")]
        [ResponseType(typeof(IEnumerable<CLastActiveDateDto>))]
        public IHttpActionResult GetContactsLastActiveDate([FromUri]Guid ownerId)
        {
            if (ownerId == default(Guid))
            {
                ModelState.AddModelError($"{nameof(ownerId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new ArgumentNullException(nameof(ownerId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var contactInfos = _userDataProvider.GetContactsLastActiveDate(ownerId);

            if (contactInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({ownerId})", new Exception("Failed to get contacts without dialogs"));
                return NotFound();
            }

            return Ok(contactInfos.Select(x => new CLastActiveDateDto(x.Id, x.LastActiveDate)));
        }
    }

}
