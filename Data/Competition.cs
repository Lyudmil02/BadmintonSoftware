using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BadmintonSoftware.Data
{
    public class Competition
    {
        public Competition()
        {
            Players = new List<Register>();   
        }

        [Key]
        public int CompetitionId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CompetitionName { get; set;}
        [DataType(DataType.Date)]
        public DateTime StartDate{ get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int MaxAge { get; set; }
        public ICollection<Register> Players{ get; set; }
    }
}
