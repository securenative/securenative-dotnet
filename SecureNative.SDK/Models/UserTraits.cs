using System;
using Newtonsoft.Json;

namespace SecureNative.SDK.Models
{
    public class UserTraits
    {
        [JsonProperty("name", NullValueHandling=NullValueHandling.Ignore)]
        private string Name { get; set; }
        [JsonProperty("email", NullValueHandling=NullValueHandling.Ignore)]
        private string Email { get; set; }
        [JsonProperty("phone", NullValueHandling=NullValueHandling.Ignore)]
        private string Phone { get; set; }
        [JsonProperty("createdAt", NullValueHandling=NullValueHandling.Ignore)] 
        private DateTime? CreatedAt { get; set; }

        public UserTraits(string name)
        {
            Name = name;
        }

        public UserTraits(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public UserTraits(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public UserTraits(string name, string email, string phone, DateTime createdAt)
        {
            Name = name;
            Email = email;
            Phone = phone;
            CreatedAt = createdAt;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetName(string value)
        {
            Name = value;
        }

        public string GetEmail()
        {
            return Email;
        }

        public void SetEmail(string value)
        {
            Email = value;
        }

        public string GetPhone()
        {
            return Phone;
        }

        public void SetPhone(string value)
        {
            Phone = value;
        }

        public DateTime? GetCreatedAt()
        {
            return CreatedAt;
        }

        public void SetCreatedAt(DateTime value)
        {
            CreatedAt = value;
        }
    }
}
