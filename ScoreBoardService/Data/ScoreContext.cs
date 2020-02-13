using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScoreBoardService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoardService.Data
{
    public class ScoreContext : DbContext
    {

        public DbSet<Player> Players { get; set; }

        public ScoreContext() { }

        public ScoreContext(DbContextOptions<ScoreContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }
  
}
