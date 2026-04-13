using System;

namespace AuthService.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string role { get; set; }
    }
}
