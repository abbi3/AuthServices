using System;

namespace AuthService.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string role { get; set; }
<<<<<<< HEAD
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
=======
>>>>>>> 747e248aae7ae30a8fa30ea97bf166d4ddc85a78
    }
}
