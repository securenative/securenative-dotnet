using System;
using System.Linq;
using Newtonsoft.Json;

namespace SecureNative.SDK.Models
{
    public class VerifyResult
    {
        [JsonProperty("riskLevel", NullValueHandling=NullValueHandling.Ignore)]
        public string RiskLevel { get; set; }
        [JsonProperty("score", NullValueHandling=NullValueHandling.Ignore)]
        public float Score { get; set; }
        [JsonProperty("triggers", NullValueHandling=NullValueHandling.Ignore)]
        public string[] Triggers { get; set; }

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
            return "Risk Level: " + RiskLevel + ", Score: " + Score + ", Triggers: [" + string.Join(",", Triggers) + "]";
        }
    }
}
