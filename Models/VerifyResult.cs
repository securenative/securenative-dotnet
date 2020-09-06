using System;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Models
{
    public class VerifyResult
    {
        public RiskLevel RiskLevel { get; set; }
        public float Score { get; set; }
        public string[] Triggers { get; set; }

        public VerifyResult()
        {
        }

        public VerifyResult(RiskLevel riskLevel, float score, string[] triggers)
        {
            this.RiskLevel = riskLevel;
            this.Score = score;
            this.Triggers = triggers;
        }
    }
}
