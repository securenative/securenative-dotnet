using SecureNative.SDK.Models;
using System;

namespace SecureNative.SDK.Interfaces
{
    public interface ISecureNative
    {
        void Track(EventOptions eventOptions);
        RiskResult Verify(EventOptions eventOptions);
        RiskResult Flow(long flowId, EventOptions eventOptions);

    }
}