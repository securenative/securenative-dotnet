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

        public RiskLevel GetRiskLevel()
        {
            return this.RiskLevel;
        }

        public void SetRiskLevel(RiskLevel value)
        {
            this.RiskLevel = value;
        }

        public float GetScore()
        {
            return this.Score;
        }

        public void SetScore(float value)
        {
            this.Score = value;
        }

        public string[] GetTriggers()
        {
            return this.Triggers;
        }

        public void SetTriggers(string[] value)
        {
            this.Triggers = value;
        }

        public override string ToString()
        {
            return "Risk Level: " + this.RiskLevel.ToString() + "Score: " + this.Score.ToString() + "Trigger: " + this.Triggers.ToString();
        }
    }
}
