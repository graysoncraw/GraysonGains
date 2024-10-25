using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraysonGains.Models.Entities
{
    public class UserPRs
    {
        [Key]
        public Guid LiftPRId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; } // Nav Property

        [Required]
        public int BenchPR { get; set; }

        [Required]
        public int SquatPR { get; set; }

        [Required]
        public int DLPR { get; set; }

        [Required]
        public int SPPR { get; set; }
    }
}
