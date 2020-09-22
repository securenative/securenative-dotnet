using System;

namespace SecureNative.SDK.Utils
{
    public static class DateUtils
    {
        public static string ToTimestamp(DateTime dateTime)
        {
            if (dateTime == null)
            {
                dateTime = new DateTime();
            }
            return dateTime.ToUniversalTime().ToString("s") + "Z";
        }

        public static String GenerateTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
