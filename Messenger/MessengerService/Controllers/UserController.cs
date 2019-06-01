using DataStorage.DataProviders;
using DTO;
using log4net;
using Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Common.ServiceLocator;
using DataStorage;

namespace MessengerService.Controllers
{
    public class UserController : ApiController
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private readonly ICUserInfoDataProvider _userDataProvider;

        public UserController()
        {
            var container = SServiceLocator.CreateContainer();
            ConfigureContainer(ref container);
            _userDataProvider = container.Resolve<ICUserInfoDataProvider>();
        }

        private static void ConfigureContainer(ref CContainer container)
        {
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<CDataStorageSettings>(ELifeCycle.Transient);
        }

        //[Route("api/user/{UserId}/lastActive?current={LastActiveDate}")]
        [Route("api/user/lastActiveDate")]
        [ResponseType(typeof(Boolean))]
        [ValidateModel]
        public IHttpActionResult UpdateLastActiveDate([FromBody]CLastActiveDateDto lastActiveDate)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({lastActiveDate}) is called");

            if (lastActiveDate == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({lastActiveDate})", new ArgumentNullException(nameof(lastActiveDate), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(lastActiveDate)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            Int32 result = 0;
            result = _userDataProvider.UpdateUserLastActiveDate(lastActiveDate.UserId, lastActiveDate.LastActiveDate);

            if (result == 0)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({lastActiveDate})", new Exception("Failed to update user last active date"));
                return NotFound();
            }

            return Ok(true);
        }

        //[Route("api/user/{UserId}/status?current={ActivityStatus}")]
        [Route("api/user/activityStatus")]
        [ResponseType(typeof(Boolean))]
        [ValidateModel]
        public IHttpActionResult UpdateActivityStatus([FromBody]CActivityStatusDto currentStatus)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({currentStatus}) is called");

            if (currentStatus == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({currentStatus})", new ArgumentNullException(nameof(currentStatus), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(currentStatus)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            Int32 result = 0;
            result = _userDataProvider.UpdateUserStatus(currentStatus.UserId, currentStatus.ActivityStatus);

            if (result == 0)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({currentStatus})", new Exception("Failed to update user status"));
                return NotFound();
            }

            return Ok(true);
        }

        [Route("api/user/{userId}/data")]
        [ResponseType(typeof(CUserDto))]
        [ValidateModel]
        public IHttpActionResult GetUserData([FromUri]Guid userId)
        {
            s_log.LogInfo($"{nameof(GetUserData)}({userId}) is called");

            if (userId == default(Guid))
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({userId})", new ArgumentNullException(nameof(userId), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(userId)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var userInfo = _userDataProvider.GetUserData(userId);
           
            if (userInfo == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({userId})", new Exception("Failed to get user data"));
                return NotFound();
            }

            return Ok(new CUserDto(userInfo.Login, userInfo.LastActiveDate, userInfo.ActivityStatus));
        }
    }
}
