using System;
namespace SecureNative.SDK.Models
{
    public class UserTraits
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserTraits(string name)
        {
            this.Name = name;
        }

        public UserTraits(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public UserTraits(string name, string email, string phone)
        {
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
        }

        public UserTraits(string name, string email, string phone, DateTime createdAt)
        {
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.CreatedAt = createdAt;
        }
    }
}
