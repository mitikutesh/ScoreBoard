using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoardService.Models
{
    public class ScoreModel
    {
        public int Score { get; set; }
        public List<string> PlayerNames { get; set; }

        public ScoreModel()
        {
            PlayerNames = new List<string>();
        }
    }
}
