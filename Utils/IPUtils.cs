using System;
namespace SecureNative.SDK.Utils
{
    public static class IPUtils
    {
        private static readonly string VALID_IPV4_PATTERN = "(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.){3}([01]?\\d\\d?|2[0-4]\\d|25[0-5])";
        private static readonly string VALID_IPV6_PATTERN = "([0-9a-f]{1,4}:){7}([0-9a-f]){1,4}";

        public static Boolean IsIpAddress(string ipAddress)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static Boolean IsValidPublicIp(string ip)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public static Boolean IsLoopBack(string ip)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}
