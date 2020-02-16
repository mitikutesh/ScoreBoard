using Microsoft.EntityFrameworkCore;

namespace ScoreBoard.API.Persistence
{
    public class ScoreContext : DbContext
    {

        public DbSet<Score> Scores { get; set; }

        public ScoreContext() { }

        public ScoreContext(DbContextOptions<ScoreContext> options) : base(options) { }
    }
  
}
