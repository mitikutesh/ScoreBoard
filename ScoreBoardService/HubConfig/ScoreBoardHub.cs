using Microsoft.AspNetCore.SignalR;
using ScoreBoardService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoardService.HubConfig
{
    public class ScoreBoardHub : Hub
    {
        public async Task BroadcastChartData(List<ScoreModel> data) => await Clients.All.SendAsync("broadcastchartdata", data);
    }
}
