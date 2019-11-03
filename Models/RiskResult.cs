namespace SecureNative.SDK.Models
{
    public class RiskResult
    {
        public RiskResult(string riskLevel, double score, string[] triggers)
        {
            RiskLevel = riskLevel;
            Score = score;
            Triggers = triggers;
        }

        public string RiskLevel { get; set; }
        public double Score { get; set; }
        public string[] Triggers { get; set; }
    }
}