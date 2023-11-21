using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BadmintonSoftware.Data
{
    public class Player
    {
        public Player()
        {
            Competitions=new List<Register>();  
        }
        [Key]
        public int PlayerId { get; set; }
        [Required]  
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public int Age { get; set; }
        public int TotalWins { get; set; } = 0;

        public int ClubId { get; set; }
        public Club Club { get; set; }

        public ICollection<Register> Competitions{ get; set; }

    }
}
