using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScoreBoard.API.HubConfig;
using ScoreBoard.API.Models;
using ScoreBoard.API.Services;
using ScoreBoard.API.TimerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreBoard.API.Controllers
{
    [Route("[controller]")]
    public class ScoreBoardController : ControllerBase
    {
        private IHubContext<ScoreBoardHub> _hub;

        private IScoreBoardService _scoreBoardService;

        public ScoreBoardController(IHubContext<ScoreBoardHub> hub, IScoreBoardService scoreBoardService)
        {
            _hub = hub;
            _scoreBoardService = scoreBoardService;
        }
        // GET: api/<controller>
        //[HttpGet]
        //public async Task<IActionResult> GetAsync()
        //{
        //    var scores = await _scoreBoardService.GetSingalAsync();
        //    List<ScoreViewModel> vm = new List<ScoreViewModel>();
        //    foreach (var item in scores)
        //    {
        //        var k = new ScoreViewModel
        //        {
        //            Name = item.Name,
        //            Point = item.Point,
        //            SignalStamp = Guid.NewGuid().ToString()
        //        };
        //        vm.Add(k);
        //    }

        //    var data = vm.OrderByDescending(a => a.Point).FirstOrDefault();

        //    var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("SignalMessageRecieved", data));

        //    //await _hub.Clients.All.SendAsync("SignalMessageRecieved", vm);
        //    return Ok(new { Message = "Request Completed" });
        //}

        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(402)]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> PostAsync([FromBody]ScoreModel scoreModel)
        {
            var saveResult = await _scoreBoardService.SaveSignalAsync(scoreModel);
            if(saveResult)
            {
                ScoreViewModel scoreView = new ScoreViewModel
                {
                    Name = scoreModel.Name,
                    Point = scoreModel.Point,
                    SignalStamp = Guid.NewGuid().ToString()
                };


                await _hub.Clients.All.SendAsync("SignalMessageRecieved", scoreView);

            }
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
