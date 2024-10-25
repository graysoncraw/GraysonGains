using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraysonGains.Models.Entities
{
    public class WorkoutLogs
    {
        [Key]
        public Guid LogId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; } // Nav Property

        public string WorkoutName { get; set; }

        public int WorkoutWeight { get; set; }

        public int WorkoutReps { get; set; }

        [Required]
        public DateTime WorkoutDate { get; set; }

    }
}
