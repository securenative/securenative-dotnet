using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Config
{
    public static class ConfigurationManager
    {
        private static readonly string RootDir = Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName.Replace("/bin", "");
        private static readonly string DefaultConfigFile = RootDir + @"/securenative.json";
        private const string CustomConfigFileEnvName = "SECURENATIVE_CONFIG_FILE";
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static SecureNativeOptions LoadConfig()
        {
            var properties = new JObject();
            try
            {
                var resourceFilePath = (string)GetEnvOrDefault(CustomConfigFileEnvName, DefaultConfigFile);
                properties = ReadResourceFile(resourceFilePath);
            }
            catch (Exception)
            {
                try
                {
                    properties = ReadResourceFile(Directory.GetParent(Environment.CurrentDirectory).FullName + @"/securenative.json");
                }
                catch (Exception e)
                {
                    Logger.Warn("Could not find config file, using default options");
                }
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

        private static string GetPropertyOrEnvOrDefault(JObject properties, string key, object defaultValue)
        {
            object res = properties.GetValue(key);
            return res == null ? GetEnvOrDefault(key, defaultValue).ToString() : res.ToString();
        }

        private static SecureNativeOptions GetOptions(JObject properties)
        {
            var builder = SecureNativeConfigurationBuilder.DefaultConfigBuilder();
            var defaultOptions = builder.Build();

            var failStretagey = GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_FAILOVER_STRATEGY", defaultOptions.GetFailOverStrategy());
            var strategy = failStretagey switch
            {
                "fail-open" => FailOverStrategy.FAIL_OPEN,
                "fail-closed" => FailOverStrategy.FAIL_CLOSED,
                _ => FailOverStrategy.FAIL_OPEN,
            };
            builder.WithApiKey(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_API_KEY", defaultOptions.GetApiKey()))
                    .WithApiUrl(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_API_URL", defaultOptions.GetApiUrl()))
                    .WithInterval(int.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_INTERVAL", defaultOptions.GetInterval())))
                    .WithMaxEvents(int.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_MAX_EVENTS", defaultOptions.GetMaxEvents())))
                    .WithTimeout(int.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_TIMEOUT", defaultOptions.GetTimeout())))
                    .WithAutoSend(bool.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_AUTO_SEND", defaultOptions.IsAutoSend())))
                    .WithDisable(bool.Parse(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_DISABLE", defaultOptions.IsDisabled())))
                    .WithLogLevel(GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_LOG_LEVEL", defaultOptions.GetLogLevel()))
                    .WithFailOverStrategy(strategy);

            return builder.Build();
        }

        private static object GetEnvOrDefault(string envName, object defaultValue)
        {
            var envValue = Environment.GetEnvironmentVariable(envName);
            return envValue ?? defaultValue;
        }
    }
}
