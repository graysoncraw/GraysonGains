using System.ComponentModel.DataAnnotations;

namespace GraysonGains.Models
{
    public class UsersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int HeightFeet { get; set; }
        public int HeightInches { get; set; }
        public float Weight { get; set; }
    }
}
