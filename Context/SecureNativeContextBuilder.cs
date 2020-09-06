using System;

namespace SecureNative.SDK.Context
{
    public class SecureNativeContextBuilder
    {
        public SecureNativeContextBuilder()
        {
        }

        public static SecureNativeContextBuilder DefaultContextBuilder()
        {
            return new SecureNativeContextBuilder();
        }

        public SecureNativeContext Build()
        {
            return new SecureNativeContext();
        }
    }
}
