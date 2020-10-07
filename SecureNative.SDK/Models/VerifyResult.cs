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
            RiskLevel = riskLevel;
            Score = score;
            Triggers = triggers;
        }

        public RiskLevel GetRiskLevel()
        {
            return RiskLevel;
        }

        public void SetRiskLevel(RiskLevel value)
        {
            RiskLevel = value;
        }

        public float GetScore()
        {
            return Score;
        }

        public void SetScore(float value)
        {
            Score = value;
        }

        public string[] GetTriggers()
        {
            return Triggers;
        }

        public void SetTriggers(string[] value)
        {
            Triggers = value;
        }

        public override string ToString()
        {
            return "Risk Level: " + RiskLevel + "Score: " + Score + "Trigger: " + Triggers;
        }
    }
}
