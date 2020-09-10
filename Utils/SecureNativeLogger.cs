using NLog;

namespace securenative_dotnet.Utils
{
    public class SecureNativeLogger
    {
        public SecureNativeLogger()
        {
        }

        public static void InitLogger(LogLevel logLevel)
        {
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                rule.SetLoggingLevels(logLevel, LogLevel.Fatal);
            }

            LogManager.ReconfigExistingLoggers();
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
