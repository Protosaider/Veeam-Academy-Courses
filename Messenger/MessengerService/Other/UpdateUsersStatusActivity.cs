using Common.ServiceLocator;
using DataStorage.DataProviders;
using log4net;
using Newtonsoft.Json;
using Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;

namespace MessengerService.Other
{
    #region UpdateUsersStatusActivity

    //public static class UpdateUsersStatusActivity
    //{
    //    private static readonly ILog s_logger = SLogger.GetLogger();
    //    private static readonly TimeSpan s_cooldown = TimeSpan.FromSeconds(10);
    //    private static readonly TimeSpan s_isNowOfflineTimeLimit = TimeSpan.FromMinutes(5);
    //    private static readonly CancellationTokenSource s_tokenSource = new CancellationTokenSource();

    //    private static readonly ICUserInfoDataProvider s_userDataProvider;

    //    static UpdateUsersStatusActivity()
    //    {
    //        var container = SServiceLocator.CreateContainer();
    //        container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
    //        container.Register<DataStorage.CDataStorageSettings>(ELifeCycle.Transient);
    //        s_userDataProvider = container.Resolve<ICUserInfoDataProvider>();
    //    }

    //    private static void UpdateUserStatus()
    //    {
    //        var timeLimitToExceed = DateTimeOffset.Now - s_isNowOfflineTimeLimit;

    //        foreach (var user in s_userDataProvider.GetAllNotOfflineUsers())
    //        {
    //            if (user.LastActiveDate < timeLimitToExceed)
    //            {
    //                s_userDataProvider.UpdateUserStatus(user.Id, 0);
    //            }
    //        }
    //    }

    //    private static void ThreadCancellation(Object obj)
    //    {
    //        s_logger.LogInfo("Thread: start");

    //        CancellationToken token = (CancellationToken)obj;
    //        try
    //        {
    //            token.ThrowIfCancellationRequested();
    //            s_logger.LogInfo("Thread: running");

    //            UpdateUserStatus();

    //            var thread = new Thread((x) =>
    //            {
    //                Thread.Sleep(s_cooldown);
    //                ThreadCancellation(x);
    //            });
    //            thread.IsBackground = true;
    //            thread.Start(token);
    //        }
    //        catch (Exception exc)
    //        {
    //            s_logger.LogInfo($"Thread: exc '{exc.Message}'");
    //            s_logger.LogInfo($"exception");
    //        }

    //        s_logger.LogInfo("Thread: end");
    //    }

    //    public static void Run()
    //    {
    //        try
    //        {
    //            s_logger.LogInfo($"Activity {nameof(UpdateUsersStatusActivity)} is started");

    //            Thread thread = new Thread(ThreadCancellation);
    //            thread.IsBackground = true;
    //            thread.Start(s_tokenSource.Token);
    //        }
    //        catch (Exception exc)
    //        {
    //            s_logger.LogError($"Activity {nameof(UpdateUsersStatusActivity)} has catched exception", exc);
    //        }
    //    }

    //    public static void Cancel()
    //    {
    //        s_tokenSource.Cancel();
    //        s_tokenSource.Dispose();
    //    }
    //}

    #endregion

    public static class SUpdateUsersStatusActivity
    {
        private static readonly ILog s_logger = SLogger.GetLogger();
        private static readonly TimeSpan s_timeout = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan s_isNowOfflineTimeLimit = TimeSpan.FromMinutes(1);
        private static readonly CancellationTokenSource s_tokenSource = new CancellationTokenSource();

        private static readonly HttpClient _client;

        static SUpdateUsersStatusActivity()
        {
            var container = SServiceLocator.CreateContainer();
            container.Register<HttpClient>(ELifeCycle.Transient);

            _client = container.Resolve<HttpClient>();

            String maintenanceServiceBaseAddress = "http://localhost:9000/";

            _client.BaseAddress = new Uri(maintenanceServiceBaseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void Run()
        {
            try
            {
                s_logger.LogInfo($"Activity {nameof(SUpdateUsersStatusActivity)} is started");

                Thread thread = new Thread(ThreadCancellation);
                thread.IsBackground = true;
                thread.Start(s_tokenSource.Token);
            }
            catch (Exception exc)
            {
                s_logger.LogError($"Activity {nameof(SUpdateUsersStatusActivity)} has catched exception", exc);
            }
        }

        private static void ThreadCancellation(Object obj)
        {
            try
            {
                s_logger.LogInfo("Thread: start");

                CancellationToken token = (CancellationToken)obj;
                token.ThrowIfCancellationRequested();

                while (!token.WaitHandle.WaitOne(s_timeout))
                {
                    s_logger.LogInfo("Thread: running");
                    SendUpdateUserStatusRequest();
                }
            }
            catch (Exception exc)
            {
                s_logger.LogInfo($"Thread: exc '{exc.Message}'");
                s_logger.LogInfo($"exception");
            }

            s_logger.LogInfo("Thread: end");
        }
        
        private static void SendUpdateUserStatusRequest()
        {
            try
            {
                HttpResponseMessage response = _client.PostAsync(
                    $"api/maintenance/updateUsersStatus",
                    new StringContent(JsonConvert.SerializeObject(s_isNowOfflineTimeLimit), Encoding.UTF8, "application/json")
                    ).Result;

                var result = response.IsSuccessStatusCode
                    ? response.Content.ReadAsAsync<Boolean>().Result
                    : false;

                if (result)
                    Console.WriteLine(@"User's statuses have been updated");
                else
                    Console.WriteLine(@"Users statuses weren't changed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void Cancel()
        {
            s_tokenSource.Cancel();
            s_tokenSource.Dispose();
        }
    }
}