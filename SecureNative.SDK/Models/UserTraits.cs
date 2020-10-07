﻿using System;

namespace SecureNative.SDK.Models
{
    public class UserTraits
    {
        private string Name { get; set; }
        private string Email { get; set; }
        private string Phone { get; set; }
        private DateTime CreatedAt { get; set; }

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

        public void SeEmail(string value)
        {
            Email = value;
        }

        public string GePhone()
        {
            return Phone;
        }

        public void SetPhone(string value)
        {
            Phone = value;
        }

        public DateTime GeCreatedAt()
        {
            return CreatedAt;
        }

        public void SetCreatedAt(DateTime value)
        {
            CreatedAt = value;
        }
    }
}
