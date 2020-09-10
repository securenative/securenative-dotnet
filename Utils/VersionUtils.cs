using System;
using System.Diagnostics;
using System.Reflection;

namespace SecureNative.SDK.Utils
{
    public static class VersionUtils
    {
        public static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;

            if (version == null || version.Equals(""))
            {
                return "unknown";
            }

            return version;
        }
    }
}
