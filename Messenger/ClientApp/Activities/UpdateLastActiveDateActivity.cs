using ClientApp.DataSuppliers;
using ClientApp.ServiceProxies;
using System;
using System.Threading;

namespace ClientApp.Activities
{
	internal sealed class CUpdateLastActiveDateActivity
    {
        private readonly IUserSupplier _userSupplier = CUserSupplier.Create();
        //private readonly TimeSpan _timeout = TimeSpan.FromMinutes(1);
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        //public void Run(Object obj)
        //{
        //    _userSupplier.UpdateLastActiveDate(TokenProvider.Id, DateTimeOffset.Now);

        //    Task.Factory.StartNew(
        //        async () => {
        //            await Task.Delay(_timeout);
        //        }, 
        //        _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        //}

        public void Run()
        {
			var thread = new Thread(UpdateLastActiveDate)
			{
				IsBackground = true
			};
			thread.Start(_tokenSource.Token);
        }

        public void Cancel()
        {
            _tokenSource.Cancel();
        }

        private void UpdateLastActiveDate(Object obj)
        {
            Console.WriteLine($@"{nameof(UpdateLastActiveDate)} is started");

            CancellationToken token = (CancellationToken)obj;
            try
            {
                token.ThrowIfCancellationRequested();

                _userSupplier.UpdateLastActiveDate(STokenProvider.Id, DateTimeOffset.Now);
                //TODO Create separate method for Activity
                _userSupplier.UpdateActivityStatus(STokenProvider.Id, 1);

				var thread = new Thread((x) =>
				{
					Thread.Sleep(_timeout);
					UpdateLastActiveDate(x);
				})
				{
					IsBackground = true
				};
				thread.Start(obj);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine($@"{nameof(UpdateLastActiveDate)} is ended");
        }
    }
}
