using Microsoft.EntityFrameworkCore;
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


        public async Task<List<Score>> GetScoreAsync()
        => await _scoreContext.Scores.ToListAsync();


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
