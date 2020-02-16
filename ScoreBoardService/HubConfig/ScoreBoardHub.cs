using Microsoft.AspNetCore.SignalR;
using ScoreBoard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoard.API.HubConfig
{
    public class ScoreBoardHub : Hub
    {
        public ScoreBoardHub()
        {

        }
        //public async Task BroadcastChartData(List<ScoreModel> data) => await Clients.All.SendAsync("broadcastchartdata", data);
    }
}
