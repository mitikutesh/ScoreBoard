using ScoreBoardService.Data;
using ScoreBoardService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoardService.Services
{
    public class ScoreBoardService
    {
        private ScoreContext _scoreContext;

        public ScoreBoardService(ScoreContext scoreContext)
        {
            _scoreContext = scoreContext;
        }

        public  void Add(string name)
        {
            var player = new Player
            {
                Name = name
            };
            _scoreContext.Add(player);
        }

        public IEnumerable<Player> Find(string name)
            => _scoreContext.Players.Where(a => a.Name.Contains(name)).ToList();

        public List<ScoreModel> GetScoreModels()
        {
            var players = _scoreContext.Players.ToList();

            var scores = new List<ScoreModel>();
            foreach (var player in players)
            {
                ScoreModel scoreMode = new ScoreModel
                {
                    Score = player.Score
                };
                scoreMode.PlayerNames.Add(player.Name);
                scores.Add(scoreMode);
            }
            return scores;
        }
    }
}
