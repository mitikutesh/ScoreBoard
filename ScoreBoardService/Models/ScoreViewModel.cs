using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoard.API.Models
{
    public class ScoreViewModel
    {
        public string Name { get; set; }
        public int Point { get; set; }
        public string SignalStamp { get; set; }
    }
}
