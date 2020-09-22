using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Config
{
    public class ConfigurationManager
    {
        private static string rootDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName.Replace("/bin", "");
        private readonly static string DEFAULT_CONFIG_FILE = rootDir + @"/securenative.json";
        private readonly static string CUSTOM_CONFIG_FILE_ENV_NAME = "SECURENATIVE_CONFIG_FILE";
        private readonly static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ConfigurationManager()
        {
        }

        public static SecureNativeOptions LoadConfig()
        {
            var properties = new JObject();
            try
            {
                string resourceFilePath = (string)GetEnvOrDefault(CUSTOM_CONFIG_FILE_ENV_NAME, DEFAULT_CONFIG_FILE);
                properties = ReadResourceFile(resourceFilePath);
            }
            catch (Exception)
            {
                Logger.Warn("Could not find config file, using default options");
            }
            
            return GetOptions(properties);

        }

        public static SecureNativeOptions LoadConfig(string path)
        {
            var properties = new JObject();
            try
            {
                properties = ReadResourceFile(path);
            } catch (Exception) {
                Logger.Warn("Could not find config file, using default options");
            }
            
            return GetOptions(properties);
        }

        private static JObject ReadResourceFile(string path)
        {
            using StreamReader file = File.OpenText(path);
            using JsonTextReader reader = new JsonTextReader(file);
            return (JObject)JToken.ReadFrom(reader);
        }

        public static SecureNativeConfigurationBuilder ConfigBuilder()
        {
            return SecureNativeConfigurationBuilder.DefaultConfigBuilder();
        }

        private static string GetPropertyOrEnvOrDefault(JObject properties, string key, Object defaultValue)
        {
            Object res = properties.GetValue(key);

            if (res == null)
            {
                return GetEnvOrDefault(key, defaultValue).ToString();
            }
            return res.ToString();
        }

        private static SecureNativeOptions GetOptions(JObject properties)
        {
            SecureNativeConfigurationBuilder builder = SecureNativeConfigurationBuilder.DefaultConfigBuilder();
            SecureNativeOptions defaultOptions = builder.Build();

            var failStretagey = GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_FAILOVER_STRATEGY", defaultOptions.GetFailoverStrategy());
            var strategy = failStretagey switch
            {
                "fail-open" => FailOverStrategy.FAIL_OPEN,
                "fail-closed" => FailOverStrategy.FAIL_CLOSED,
                _ => FailOverStrategy.FAIL_OPEN,
            };
            builder.WithApiKey(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_API_KEY", defaultOptions.GetApiKey()))
                    .WithApiUrl(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_API_URL", defaultOptions.GetApiUrl()))
                    .WithInterval(Int32.Parse((string)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_INTERVAL", defaultOptions.GetInterval())))
                    .WithMaxEvents(Int32.Parse((string)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_MAX_EVENTS", defaultOptions.GetMaxEvents())))
                    .WithTimeout(Int32.Parse((string)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_TIMEOUT", defaultOptions.GetTimeout())))
                    .WithAutoSend(bool.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_AUTO_SEND", defaultOptions.IsAutoSend())))
                    .WithDisable(bool.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_DISABLE", defaultOptions.IsDisabled())))
                    .WithLogLevel(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_LOG_LEVEL", defaultOptions.GetLogLevel()))
                    .WithFailoverStrategy(strategy);

            return builder.Build();
        }

        private static Object GetEnvOrDefault(string envName, Object defaultValue)
        {
            var envValue = Environment.GetEnvironmentVariable(envName);
            if (envValue != null)
            {
                return envValue;
            }

            return defaultValue;
        }
    }
}
