using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraysonGains.Models.Entities
{
    public class UserWorkoutDays
    {
        [Key]
        public Guid WorkoutDayId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; } // Nav Property

        [Required]
        [MaxLength(10)]
        public string BenchDay { get; set; }

        [Required]
        [MaxLength(10)]
        public string SquatDay { get; set; }

        [Required]
        [MaxLength(10)]
        public string DLDay { get; set; }

        [Required]
        [MaxLength(10)]
        public string SPDay { get; set; }

        [Required]
        public DateTime CycleStart { get; set; }
    }
}
