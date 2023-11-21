using System.ComponentModel.DataAnnotations;

namespace BadmintonSoftware.Data
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }
        public string ClubName { get; set; }
    }
}
