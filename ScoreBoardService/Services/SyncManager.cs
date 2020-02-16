using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScoreBoard.API.Services
{
    public class SyncManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime Start { get; set; }

        public SyncManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(DoWork, _autoResetEvent, 1000, 2000);
            Start = DateTime.Now;

        }

        public void DoWork(object stateInfo)
        {
            _action();

            if ((DateTime.Now - Start).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}
