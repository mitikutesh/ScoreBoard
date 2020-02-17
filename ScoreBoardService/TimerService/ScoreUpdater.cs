using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    public class ScoreUpdater : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer;
 

        public ScoreUpdater(IServiceProvider services)
        {
            Services = services;
        }
        public IServiceProvider Services { get; }
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(async (e) => await DoWorkAsync(e, stoppingToken), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync(object state, CancellationToken stoppingToken)
        {
            var count = Interlocked.Increment(ref executionCount);
            //,IHubContext<ScoreBoardHub> hub, IScoreBoardService scoreBoardService
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedScoreUpdater>();
                var hub = scope.ServiceProvider.GetRequiredService<IHubContext<ScoreBoardHub>>();
                var scoreBoardService = scope.ServiceProvider.GetRequiredService<IScoreBoardService>();
                await scopedProcessingService.ExcuteAsync(stoppingToken, hub, scoreBoardService);
            }
        }


        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
