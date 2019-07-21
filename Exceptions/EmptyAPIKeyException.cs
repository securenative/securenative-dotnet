using System;
using System.Collections.Generic;
using System.Text;

namespace SecureNative.SDK.Exceptions
{
    public class EmptyAPIKeyException: Exception
    {
        public EmptyAPIKeyException(string message) : base(message)
        {

        }
    }
}
