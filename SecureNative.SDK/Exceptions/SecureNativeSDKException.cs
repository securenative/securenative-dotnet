using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeSdkException : Exception
    {
        private readonly string _message;

        public SecureNativeSdkException(string message)
        {
            _message = message;
        }
    }
}
