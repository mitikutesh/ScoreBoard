using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoardService.Models
{
    public class ScoreModel
    {
        public List<int> Data { get; set; }
        public string Player { get; set; }

        public ScoreModel()
        {
            Data = new List<int>();
        }
    }
}
