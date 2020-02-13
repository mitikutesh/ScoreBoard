using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScoreBoardService.HubConfig;
using ScoreBoardService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScoreBoardService.Controllers
{
    [Route("api/[controller]")]
    public class ScoreBoardController : Controller
    {
        private IHubContext<ScoreBoardHub> _hub;
        private ScoreBoardService.Services.ScoreBoardService _scoreBoardService;

        public ScoreBoardController(IHubContext<ScoreBoardHub> hub, ScoreBoardService.Services.ScoreBoardService scoreBoardService)
        {
            _hub = hub;
            _scoreBoardService = scoreBoardService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var timerManager = new SyncManager(() => _hub.Clients.All.SendAsync("transferdata", _scoreBoardService.GetScoreModels()));

            return Ok(new { Message = "Request Completed" });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
