using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SecureNative.SDK.Utils
{
    public static class IpUtils
    {
        private const string ValidIpv4Pattern = "(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.){3}([01]?\\d\\d?|2[0-4]\\d|25[0-5])";
        private const string ValidIpv6Pattern = "([0-9a-f]{1,4}:){7}([0-9a-f]){1,4}";

        public static bool IsIpAddress(string ipAddress)
        {
            var match = Regex.IsMatch(ipAddress, ValidIpv4Pattern);
            if (match)
            {
                return true;
            }

            match = Regex.IsMatch(ipAddress, ValidIpv6Pattern);
            return match;
        }

        public static bool IsValidPublicIp(string ipAddress)
        {
            try
            {
                var address = IPAddress.Parse(ipAddress);
                if (IPAddress.IsLoopback(address)) return false;
                else if (address.ToString() == "::1") return true;

                byte[] bytes = address.GetAddressBytes();
                return (bytes[0]) switch
                {
                    10 => false,
                    172 => bytes[1] < 32 && bytes[1] >= 16,
                    192 => bytes[1] == 168,
                    _ => true,
                };
            } catch (Exception) {
                return false;
            }
        }

        public static bool IsLoopBack(string ipAddress)
        {
            try
            {
                var address = IPAddress.Parse(ipAddress);
                return IPAddress.IsLoopback(address);
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
