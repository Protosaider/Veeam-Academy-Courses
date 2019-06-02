using Common.ServiceLocator;
using DataStorage.DataProviders;
using log4net;
using System;
using System.Threading;

namespace MaintenanceService.Other
{
    public static class SUpdateUsersStatusActivity
    {
        private static readonly ILog s_logger = SLogger.GetLogger();
        //private static readonly TimeSpan s_cooldown = TimeSpan.FromSeconds(60);
        private static readonly TimeSpan s_cooldown = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan s_isNowOfflineTimeLimit = TimeSpan.FromMinutes(5);
        private static readonly CancellationTokenSource s_tokenSource = new CancellationTokenSource();

        private static readonly ICUserInfoDataProvider s_userDataProvider;

        static SUpdateUsersStatusActivity()
        {
            var container = SServiceLocator.CreateContainer();
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<DataStorage.CDataStorageSettings>(ELifeCycle.Transient);
            s_userDataProvider = container.Resolve<ICUserInfoDataProvider>();
        }

        private static void UpdateUserStatus()
        {
            var timeLimitToExceed = DateTimeOffset.Now - s_isNowOfflineTimeLimit;

            foreach (var user in s_userDataProvider.GetAllNotOfflineUsers())
            {
                if (user.LastActiveDate < timeLimitToExceed)
                {
                    s_userDataProvider.UpdateUserStatus(user.Id, 0);
                }
            }
        }

        private static void ThreadCancellation(Object obj)
        {
            s_logger.LogInfo("Thread: start");

            CancellationToken token = (CancellationToken)obj;
            try
            {
                token.ThrowIfCancellationRequested();
                s_logger.LogInfo("Thread: running");

                UpdateUserStatus();

                //Thread.Sleep(_cooldown);
                //var thread = new Thread(ThreadCancellation);

                var thread = new Thread((x) => {
                    Thread.Sleep(s_cooldown);
                    ThreadCancellation(x);
                });
                thread.IsBackground = true;
                thread.Start(token);
            }
            catch (Exception exc)
            {
                s_logger.LogInfo($"Thread: exc '{exc.Message}'");
                s_logger.LogInfo($"exception");
            }

            s_logger.LogInfo("Thread: end");
        }

        public static void Run()
        {
            try
            {
                s_logger.LogInfo($"Activity {nameof(SUpdateUsersStatusActivity)} is started");

                //using (CancellationTokenSource cancellationSource = new CancellationTokenSource())
                //{
                //    Thread thread = new Thread(ThreadCancellation);
                //    thread.IsBackground = true;
                //    thread.Start(cancellationSource.Token);
                //}

                Thread thread = new Thread(ThreadCancellation);
                thread.IsBackground = true;
                thread.Start(s_tokenSource.Token);
            }
            catch (Exception exc)
            {
                s_logger.LogError($"Activity {nameof(SUpdateUsersStatusActivity)} has catched exception", exc);
            }
        }

        public static void Cancel()
        {
            s_tokenSource.Cancel();
            s_tokenSource.Dispose();
        }
    }
}