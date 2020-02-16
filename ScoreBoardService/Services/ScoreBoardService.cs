using ScoreBoard.API.Models;
using ScoreBoard.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoard.API.Services
{
    public class ScoreBoardService : IScoreBoardService
    {
        private ScoreContext _scoreContext;

        public ScoreBoardService(ScoreContext scoreContext)
        {
            _scoreContext = scoreContext;
        }

        public void Add(string name)
        {
            var player = new ScoreViewModel
            {
                Name = name
            };
            _scoreContext.Add(player);
        }


        //public List<ScoreModel> GetScoreModels()
        //{
        //    var scores = _scoreContext.Scores.ToList();

        //    var scoreModels = new List<ScoreModel>();
        //    foreach (var score in scoreModels)
        //    {
        //        ScoreModel scoreMode = new ScoreModel
        //        {
        //           Name = score.Name,
        //           Point = score.Point
        //        };
        //        scoreModels.Add(scoreMode);
        //    }
        //    return scoreModels;
        //}

        public async Task<bool> SaveSignalAsync(ScoreModel scoreModel)
        {
            try
            {
                Score score = new Score()
                {
                    Name = scoreModel.Name,
                    Point = scoreModel.Point
                };
                _scoreContext.Scores.Add(score);
                return await _scoreContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
