using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Config
{
    public class ConfigurationManager
    {
        private readonly static string DEFAULT_CONFIG_FILE = @"securenative.json";
        private readonly static string CUSTOM_CONFIG_FILE_ENV_NAME = "SECURENATIVE_CONFIG_FILE";

        public ConfigurationManager()
        {
        }

        public static SecureNativeOptions LoadConfig()
        {
            string resourceFilePath = GetEnvOrDefault(CUSTOM_CONFIG_FILE_ENV_NAME, DEFAULT_CONFIG_FILE);
            var properties = ReadResourceFile(resourceFilePath);
            return GetOptions(properties);

        }

        public static SecureNativeOptions LoadConfig(string path)
        {
            var properties = ReadResourceFile(path);
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

        private static Object GetPropertyOrEnvOrDefault(JObject properties, string key, Object defaultValue)
        {
            string defaultStrValue = defaultValue?.ToString();
            Object res = properties.GetValue(key);

            if (res == null)
            {
                return GetEnvOrDefault(key, defaultStrValue);
            }
            return res;
        }

        private static SecureNativeOptions GetOptions(JObject properties)
        {
            SecureNativeConfigurationBuilder builder = SecureNativeConfigurationBuilder.DefaultConfigBuilder();
            SecureNativeOptions defaultOptions = builder.Build();

            builder.WithApiKey((string)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_API_KEY", defaultOptions.GetApiKey()))
                    .WithApiUrl((string)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_API_URL", defaultOptions.GetApiUrl()))
                    .WithInterval((int)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_INTERVAL", defaultOptions.GetInterval()))
                    .WithMaxEvents((int)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_MAX_EVENTS", defaultOptions.GetMaxEvents()))
                    .WithTimeout((int)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_TIMEOUT", defaultOptions.GetTimeout()))
                    .WithAutoSend((Boolean)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_AUTO_SEND", defaultOptions.IsAutoSend()))
                    .WithDisable((Boolean)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_DISABLE", defaultOptions.IsDisabled()))
                    .WithLogLevel((string)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_LOG_LEVEL", defaultOptions.GetLogLevel()))
                    .WithFailoverStrategy((FailOverStrategy)GetPropertyOrEnvOrDefault(properties, "SECURENATIVE_FAILOVER_STRATEGY", defaultOptions.GetFailoverStrategy()));

            return builder.Build();
        }

        private static string GetEnvOrDefault(string envName, string defaultValue)
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
