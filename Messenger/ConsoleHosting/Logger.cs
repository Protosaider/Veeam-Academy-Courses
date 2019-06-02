using System;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Config;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//#if DEBUG
//        [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.Debug.config")]
//#else
//        [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.Release.config")]
//#endif

/*
 * Who was using the system when it failed?
 * Where in the code did the application fail?
 * What was the system doing when it failed?
 * When did the failure occur?
 * Why did the application fail?
 */

namespace ConsoleHosting
{
	internal static class SLogger
    {
        //public static ILog Log { get; } = LogManager.GetLogger("WebService");
        //public static ILog Log { get; } = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static ILog Log { get; } = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitLogger()
        {
            //#if DEBUG
            //            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(@".\log4net.Debug.config"));
            //#else
            //            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(@".\log4net.Release.config"));
            //#endif

            String logFilePath;
#if DEBUG
            logFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.Debug.config");
#else
            logFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.Release.config");
#endif
            System.IO.FileInfo finfo = new System.IO.FileInfo(logFilePath);
            XmlConfigurator.ConfigureAndWatch(finfo);

            try
            {
                if (!log4net.LogManager.GetRepository().Configured)
                {
                    // log4net not configured
                    foreach (log4net.Util.LogLog message in log4net.LogManager.GetRepository().ConfigurationMessages.Cast<log4net.Util.LogLog>())
                    {
                        // evaluate configuration message
                    }
                    throw new Exception("Logger initialization failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Info("Logger initialization complete");
        }

        public static ILog GetLogger([CallerFilePath]String filename = null)
        {
            return LogManager.GetLogger(filename);
        }    
    }
}


//public static class LogFactory
//{
//    public const string ConfigFileName = "log4net.config";

//    public static void Configure()
//    {
//        Type type = typeof(LogFactory);
//        FileInfo assemblyDirectory = AssemblyInfo.GetCodeBaseDirectory(type);
//        string path = Path.Combine(assemblyDirectory.FullName, ConfigFileName);
//        FileInfo configFile = new FileInfo(path);
//        XmlConfigurator.ConfigureAndWatch(configFile);
//        log4net.ILog log = LogManager.GetLogger(type);
//        log.ToString();
//    }
//}