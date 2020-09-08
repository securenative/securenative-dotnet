using System;

namespace SecureNative.SDK.Utils
{
    public static class Utils
    {
        public static Boolean IsNullOrEmpty(string str)
        {
            return str == null || str.Length == 0;
        }
    }
}
