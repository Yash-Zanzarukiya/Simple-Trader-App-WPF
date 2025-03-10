﻿namespace SimpleTrader.Domain.Models
{
    public class User : DomainObject
    {
        public string  Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime JoinedOn { get; set; }
    }
}
