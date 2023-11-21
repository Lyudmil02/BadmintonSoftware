using System.ComponentModel.DataAnnotations;

namespace BadmintonSoftware.Data
{
    public class Compete
    {
        [Key]
        public int CompeteId { get; set; }

        public int PlayerOneId { get; set; }
        public Player PlayerOne { get; set; }

        public int PlayerTwoId { get; set; }
        public Player PlayerTwo { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
    }
}
