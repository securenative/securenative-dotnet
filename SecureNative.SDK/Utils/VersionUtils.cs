using System.Diagnostics;
using System.Reflection;

namespace SecureNative.SDK.Utils
{
    public static class VersionUtils
    {
        public static string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var version = fileVersionInfo.ProductVersion;

            if (version == null || version.Equals(""))
            {
                return "unknown";
            }

            return version;
        }
    }
}
