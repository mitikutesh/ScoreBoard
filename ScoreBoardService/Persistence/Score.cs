using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoard.API.Persistence
{
    public class Score
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int  Point { get; set; }
        public DateTime timeStamp { get; set; } = DateTime.Now;
    }
}
