using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeParseException : Exception
    {
        private readonly string _message;

        public SecureNativeParseException(string message)
        {
            _message = message;
        }
    }
}
