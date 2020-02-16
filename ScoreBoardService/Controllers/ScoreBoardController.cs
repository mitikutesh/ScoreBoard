using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScoreBoard.API.HubConfig;
using ScoreBoard.API.Models;
using ScoreBoard.API.Services;
using System;
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
        [HttpGet]
        public IActionResult Get()
        {
            //var timerManager = new SyncManager(() => _hub.Clients.All.SendAsync("transferdata", _scoreBoardService.GetScoreModels()));

            //return Ok(new { Message = "Request Completed" });
            return Ok("Working");
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

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
            return Ok(saveResult);
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
