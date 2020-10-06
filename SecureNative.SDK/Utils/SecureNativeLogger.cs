using System;
using NLog;

namespace SecureNative.SDK.Utils
{
    public class SecureNativeLogger
    {
        public SecureNativeLogger()
        {
        }

        public static void InitLogger(LogLevel logLevel)
        {
            try
            {
                foreach (var rule in LogManager.Configuration.LoggingRules)
                {
                    rule.SetLoggingLevels(logLevel, LogLevel.Fatal);
                }

                LogManager.ReconfigExistingLoggers();
            } catch (Exception)
            {
                Console.WriteLine("Could not init logger, falling to default");
            }
        }

        public static LogLevel GetLogLevel(string logLevel)
        {
            LogLevel level = (logLevel.ToLower()) switch
            {
                "debug" => LogLevel.Debug,
                "info" => LogLevel.Info,
                "error" => LogLevel.Error,
                "trace" => LogLevel.Trace,
                "warn" => LogLevel.Warn,
                "fatal" => LogLevel.Fatal,
                _ => LogLevel.Warn,
            };
            return level; 
        }
    }
}
