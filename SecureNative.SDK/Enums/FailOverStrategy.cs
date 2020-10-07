using System.ComponentModel;

namespace SecureNative.SDK.Enums
{
    public enum FailOverStrategy
    {
        [Description("fail-open")]
        FAIL_OPEN,
        [Description("fail-closed")]
        FAIL_CLOSED
    }
}
