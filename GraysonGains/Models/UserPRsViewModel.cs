using GraysonGains.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraysonGains.Models
{
    public class UserPRsViewModel
    {
        public Guid UserId { get; set; }
        public int BenchPR { get; set; }
        public int SquatPR { get; set; }
        public int DLPR { get; set; }
        public int SPPR { get; set; }
    }
}
