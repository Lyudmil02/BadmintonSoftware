using Microsoft.EntityFrameworkCore;

namespace BadmintonSoftware.Data
{
    public class BadmintonSoftwareContext:DbContext
    {
        public BadmintonSoftwareContext(DbContextOptions<BadmintonSoftwareContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Compete> Competes { get; set; }
        public DbSet<Club> Clubs { get; set; }

    }
}
