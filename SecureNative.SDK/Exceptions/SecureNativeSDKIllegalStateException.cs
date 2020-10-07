using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeSdkIllegalStateException : Exception
    {
        private readonly string _message;

        public SecureNativeSdkIllegalStateException()
        {
        }

        public SecureNativeSdkIllegalStateException(string message)
        {
            _message = message;
        }
    }
}
