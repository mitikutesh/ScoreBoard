using Microsoft.AspNetCore.SignalR;
using ScoreBoard.API.HubConfig;
using ScoreBoard.API.Models;
using ScoreBoard.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScoreBoard.API.TimerService
{
    internal interface IScopedScoreUpdater
    {
        Task ExcuteAsync(CancellationToken stoppingToken, IHubContext<ScoreBoardHub> hub, IScoreBoardService scoreBoardService);
    }

    internal class ScopedScoreUpdater : IScopedScoreUpdater
    {
        private int executionCount = 0;
        public ScopedScoreUpdater(IServiceProvider services)
        {
            Services = services;
        }
        public IServiceProvider Services { get; }

        public async Task ExcuteAsync(CancellationToken stoppingToken, IHubContext<ScoreBoardHub> hub, IScoreBoardService scoreBoardService)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;


                var scores = await scoreBoardService.GetScoreAsync();

                List<ScoreViewModel> model = new List<ScoreViewModel>();
                foreach (var item in scores)
                {
                    var temp = new ScoreViewModel
                    {
                        Name = item.Name,
                        Point = item.Point,
                        SignalStamp = Guid.NewGuid().ToString()
                    };
                    model.Add(temp);
                }

                await hub.Clients.All.SendAsync("SignalMessageRecieved", model);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
