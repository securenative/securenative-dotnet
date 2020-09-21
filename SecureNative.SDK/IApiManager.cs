using System;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public interface IApiManager
    {
        void Track(EventOptions eventOptions);
        VerifyResult Verify(EventOptions eventOptions);
    }
}
