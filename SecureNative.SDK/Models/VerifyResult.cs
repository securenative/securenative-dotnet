namespace SecureNative.SDK.Models
{
    public class VerifyResult
    {
        private string RiskLevel { get; set; }
        private float Score { get; set; }
        private string[] Triggers { get; set; }

        public VerifyResult()
        {
        }

        public VerifyResult(string riskLevel, float score, string[] triggers)
        {
            RiskLevel = riskLevel;
            Score = score;
            Triggers = triggers;
        }

        public string GetRiskLevel()
        {
            return RiskLevel;
        }

        public void SetRiskLevel(string value)
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
            var triggers = Triggers.ToString() ?? "[]";
            return "Risk Level: " + RiskLevel + " Score: " + Score + " Trigger: " + triggers;
        }
    }
}
