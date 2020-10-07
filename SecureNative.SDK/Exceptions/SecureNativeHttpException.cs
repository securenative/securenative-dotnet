using System;

namespace SecureNative.SDK.Exceptions
{
    public class SecureNativeHttpException : Exception
    {
        private readonly string _message;

        public SecureNativeHttpException(string message)
        {
            _message = message;
        }
    }
}
