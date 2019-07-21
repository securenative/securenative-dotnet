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

        private string RiskLevel { get; set; }
        private double Score { get; set; }
        private string[] Triggers { get; set; }
    }
}