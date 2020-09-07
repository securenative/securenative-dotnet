using System;

namespace SecureNative.SDK.Config
{
    public class ConfigurationManager
    {
        private readonly static string DEFAULT_CONFIG_FILE = "securenative.cnf";
        private readonly static string CUSTOM_CONFIG_FILE_ENV_NAME = "SECURENATIVE_CONFIG_FILE";

        public ConfigurationManager()
        {
        }

        public static SecureNativeOptions LoadConfig()
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static SecureNativeOptions LoadConfig(string path)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}
