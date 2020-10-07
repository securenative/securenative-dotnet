using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeInvalidUriException : Exception
    {
        private readonly string _message;

        public SecureNativeInvalidUriException(string message)
        {
            _message = message;
        }
    }
}
