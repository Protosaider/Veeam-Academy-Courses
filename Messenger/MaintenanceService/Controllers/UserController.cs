using Common.ServiceLocator;
using DataStorage.DataProviders;
using log4net;
using Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MaintenanceService.Controllers
{
    public class MaintenanceController : ApiController
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private readonly ICUserInfoDataProvider _userDataProvider;

        public MaintenanceController()
        {
            var container = SServiceLocator.CreateContainer();
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<DataStorage.CDataStorageSettings>(ELifeCycle.Transient);
            _userDataProvider = container.Resolve<ICUserInfoDataProvider>();
        }

        [Route("api/maintenance/ping")]
        [ResponseType(typeof(String))]
        public IHttpActionResult Ping()
        {
            s_log.LogInfo($"{nameof(Ping)}() is called");
            Console.WriteLine($@"{nameof(Ping)}() is called");
            return Ok("PING");
        }

        [Route("api/maintenance/updateUsersStatus")]
        [ResponseType(typeof(Boolean))]
        public IHttpActionResult UpdateUsersStatus([FromBody]TimeSpan isNowOfflineTimeLimit)
        {
            s_log.LogInfo($@"{nameof(UpdateUsersStatus)}({isNowOfflineTimeLimit}) is called");
            Console.WriteLine($@"{nameof(UpdateUsersStatus)}({isNowOfflineTimeLimit}) is called");

            var timeLimitToExceed = DateTimeOffset.Now - isNowOfflineTimeLimit;

            var notOfflineUsers = _userDataProvider.GetAllNotOfflineUsers();
            if (notOfflineUsers == null || notOfflineUsers.Count == 0)
                return Ok(false);
            foreach (var user in notOfflineUsers)
            {
                if (user.LastActiveDate < timeLimitToExceed)
                {
                    var result = _userDataProvider.UpdateUserStatus(user.Id, 0);
                    if (result == 0)
                        return Ok(false);
                }
            }
            return Ok(true);           
        }

        //Use in messenger service:
        //private readonly HttpClient _client;
        //_client = new HttpClient();
        //_client.BaseAddress = new Uri("http://localhost:9000/");
        //_client.DefaultRequestHeaders.Accept.Clear();
        //_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    try
        //    {
        //        HttpResponseMessage response = _client.GetAsync(
        //            $"api/maintenance/ping"
        //            ).Result;
        //var res = response.IsSuccessStatusCode
        //    ? response.Content.ReadAsStringAsync().Result
        //    : null;
        //Console.WriteLine("###########");
        //        Console.WriteLine(res);
        //        Console.WriteLine("###########");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
    }
}
