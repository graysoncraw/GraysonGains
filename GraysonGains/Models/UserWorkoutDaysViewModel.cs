using GraysonGains.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraysonGains.Models
{
    public class UserWorkoutDaysViewModel
    {
        public Guid UserId { get; set; }

        public string BenchDay { get; set; }

        public string SquatDay { get; set; }

        public string DLDay { get; set; }

        public string SPDay { get; set; }

        public DateTime CycleStart { get; set; }
    }
}
