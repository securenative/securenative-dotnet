using System;
using SecureNative.SDK.Enums;

namespace SecureNative.SDK.Models
{
    public class VerifyResult
    {
        private RiskLevel RiskLevel { get; set; }
        private float Score { get; set; }
        private string[] Triggers { get; set; }

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
