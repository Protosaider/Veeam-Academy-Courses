using System;
using System.Runtime.CompilerServices;
using log4net;

namespace Info.Other
{
    internal static class SLogger
    {
        public static void LogDebug(this ILog logger, String message)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(message);
        }
        public static void LogInfo(this ILog logger, String message)
        {
            if (logger.IsInfoEnabled)
                logger.Info(message);
        }
        public static void LogWarn(this ILog logger, String message)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(message);
        }
        public static void LogError(this ILog logger, String message, Exception exception)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message, exception);
        }
        public static void LogFatal(this ILog logger, String message)
        {
            if (logger.IsFatalEnabled)
                logger.Fatal(message);
        }

        public static ILog GetLogger([CallerFilePath]String filename = null)
        {
            return LogManager.GetLogger(System.Reflection.Assembly.GetCallingAssembly(), filename);
        }
    }
}
