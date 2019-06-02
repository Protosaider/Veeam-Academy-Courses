using ClientApp.Other;
using System;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
	internal sealed partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Log it
            SLogger.GetLogger().LogInfo("Application starting...");

            //// Setup the application view model based on if we are logged in
            //ViewModelApplication.GoToPage(
            //    // If we are logged in...
            //    await ClientDataStore.HasCredentialsAsync() ?
            //    // Go to chat page
            //    ApplicationPage.Chat :
            //    // Otherwise, go to login page
            //    ApplicationPage.Login);

            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.LogIn);

            var dt = DateTime.Now;
            var dt2 = DateTime.UtcNow;
            var dt3 = new DateTime(2018, 10, 4, 12, 49, 43, DateTimeKind.Local);
            var dt4 = new DateTime(2018, 10, 4, 12, 49, 43, DateTimeKind.Utc);
            var dto = DateTimeOffset.Now;
            var dto2 = DateTimeOffset.UtcNow;
            var dto3 = new DateTimeOffset(2018, 10, 4, 12, 49, 43, new TimeSpan(0, -3, 0, 0));
            

            Console.WriteLine(@"TIMING 

 +++++++++++ 

");
            Console.WriteLine(@"dt {0}, toUtc {1}, toLocal {2}", dt, dt.ToUniversalTime(), dt.ToLocalTime());
            Console.WriteLine(@"dt {0}, toUtc {1}, toLocal {2}", dt2, dt2.ToUniversalTime(), dt2.ToLocalTime());
            Console.WriteLine(@"dt {0}, toUtc {1}, toLocal {2}", dt3, dt3.ToUniversalTime(), dt3.ToLocalTime());
            Console.WriteLine(@"dt {0}, toUtc {1}, toLocal {2}", dt4, dt4.ToUniversalTime(), dt4.ToLocalTime());
            Console.WriteLine(@"dto {0}, toUtc {1}, toLocal {2}, localDT {3}, utcDT {4}, DT {5}, offset {6}", dto, dto.ToUniversalTime(), dto.ToLocalTime(), dto.LocalDateTime, dto.UtcDateTime, dto.DateTime, dto.Offset);
            Console.WriteLine(@"dto {0}, toUtc {1}, toLocal {2}, localDT {3}, utcDT {4}, DT {5}, offset {6}", dto2, dto2.ToUniversalTime(), dto2.ToLocalTime(), dto2.LocalDateTime, dto2.UtcDateTime, dto2.DateTime, dto2.Offset);
            Console.WriteLine(@"dto {0}, toUtc {1}, toLocal {2}, localDT {3}, utcDT {4}, DT {5}, offset {6}", dto3, dto3.ToUniversalTime(), dto3.ToLocalTime(), dto3.LocalDateTime, dto3.UtcDateTime, dto3.DateTime, dto3.Offset);
            Console.WriteLine(@"+++++++++++
");

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
