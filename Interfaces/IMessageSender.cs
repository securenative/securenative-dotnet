using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace SecureNative.SDK.Interfaces
{
    public interface IMessageSender<T>
    {
        string Post(string uri, T message);
    }
}
