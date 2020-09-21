using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SecureNative.SDK.Utils
{
    public static class IPUtils
    {
        private static readonly string VALID_IPV4_PATTERN = "(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.){3}([01]?\\d\\d?|2[0-4]\\d|25[0-5])";
        private static readonly string VALID_IPV6_PATTERN = "([0-9a-f]{1,4}:){7}([0-9a-f]){1,4}";

        public static Boolean IsIpAddress(string ipAddress)
        {
            var match = Regex.IsMatch(ipAddress, VALID_IPV4_PATTERN);
            if (match)
            {
                return true;
            }

            match = Regex.IsMatch(ipAddress, VALID_IPV6_PATTERN);
            if (match)
            {
                return true;
            }

            return false;
        }

        public static Boolean IsValidPublicIp(string ipAddress)
        {
            var address = new IPAddress(Encoding.ASCII.GetBytes(ipAddress));
            if (IPAddress.IsLoopback(address)) return true;
            else if (address.ToString() == "::1") return false;

            byte[] bytes = address.GetAddressBytes();
            return (bytes[0]) switch
            {
                10 => true,
                172 => bytes[1] < 32 && bytes[1] >= 16,
                192 => bytes[1] == 168,
                _ => false,
            };
        }

        public static Boolean IsLoopBack(string ipAddress)
        {
            var address = new IPAddress(Encoding.ASCII.GetBytes(ipAddress));
            return IPAddress.IsLoopback(address);
        }
    }
}
