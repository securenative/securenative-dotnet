using System;

namespace SecureNative.SDK.Utils
{
    public static class DateUtils
    {
        public static string ToTimestamp(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("s") + "Z";
        }

        public static string GenerateTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
