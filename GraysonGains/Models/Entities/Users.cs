using System.ComponentModel.DataAnnotations;

namespace GraysonGains.Models.Entities
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        public int HeightFeet { get; set; }

        [Required]
        public int HeightInches { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
