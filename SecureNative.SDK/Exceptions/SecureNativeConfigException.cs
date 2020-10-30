using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeConfigException : Exception
    {
        private readonly string _message;

        public SecureNativeConfigException(string message)
        {
            _message = message;
        }
    }
}
