using MessengerService.Other;
using Microsoft.Owin.Hosting;
using System;
using ConsoleHosting.Other;


namespace ConsoleHosting
{
    internal class Program
    {
        private static void Main(String[] args)
        {
            // test from browser as:
            // http://localhost:4221/api/{controller}/{smth}/

            // Работа со временем: https://i.stack.imgur.com/QE5xq.png
            // https://devblogs.microsoft.com/pfxteam/building-async-coordination-primitives-part-1-asyncmanualresetevent/
            //https://devblogs.microsoft.com/pfxteam/should-i-expose-asynchronous-wrappers-for-synchronous-methods/
            //https://devblogs.microsoft.com/pfxteam/implementing-a-simple-foreachasync/
            //https://codeburst.io/polling-vs-sse-vs-websocket-how-to-choose-the-right-one-1859e4e13bd9?gi=9f43977ece00

            String baseAddress = "http://localhost:4221/";

            try
            {
                // Start OWIN host
                using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(new StartOptions(baseAddress)))
                {
                    SLogger.GetLogger().LogInfo($"WebAPI SelfHost started at {baseAddress}");
                    Console.WriteLine($@"WebAPI SelfHost started at {baseAddress}");
                    Console.WriteLine(@"Press enter to finish");

                    SUpdateUsersStatusActivity.Run();

                    Console.ReadLine();
                }

                SLogger.GetLogger().LogInfo("WebAPI SelfHost closed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                SUpdateUsersStatusActivity.Cancel();
            }

        }
    }
}
