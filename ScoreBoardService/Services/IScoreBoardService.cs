using ScoreBoard.API.Models;
using ScoreBoard.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoard.API.Services
{
    public interface IScoreBoardService
    {
        Task<bool> SaveSignalAsync(ScoreModel scoreModel);
        Task<List<Score>> GetScoreAsync();
    }
}
