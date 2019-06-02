using System;
using Microsoft.Owin.Hosting;

namespace ConsoleHostingMaintenance
{
    internal class Program
    {
        private static void Main(String[] args)
        {
            // test from browser as:
            // http://localhost:4221/api/{controller}/{smth}/

            String baseAddress = "http://localhost:9000/";

            try
            {
                // Start OWIN host
                using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(new StartOptions(baseAddress)))
                {
                    Console.WriteLine($@"WebAPI SelfHost started at {baseAddress}");
                    Console.WriteLine(@"Press enter to finish");

                    //UpdateUsersStatusActivity.Run();

                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                //UpdateUsersStatusActivity.Cancel();
            }

        }
    }
}
