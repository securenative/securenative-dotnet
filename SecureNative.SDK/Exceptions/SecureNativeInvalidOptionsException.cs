using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeInvalidOptionsException : Exception
    {
        private readonly string _message;

        public SecureNativeInvalidOptionsException(string message)
        {
            _message = message;
        }
    }
}
