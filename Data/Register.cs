using System;
using System.ComponentModel.DataAnnotations;

namespace BadmintonSoftware.Data
{
    public class Register
    {
        [Key]
        public int RegisterId { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisterDate{ get; set; }
    }
}
