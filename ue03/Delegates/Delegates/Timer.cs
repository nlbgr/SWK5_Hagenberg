using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates {
    public delegate void ExpiredEventHandler(DateTime signaledTime);

    public class Timer {
        private const int DEFAULT_INTERVAL = 1000;
        private readonly Thread ticker;
        public int Interval { get; set; } = DEFAULT_INTERVAL;
        public event ExpiredEventHandler? Expired;

        public Timer() {
            this.ticker = new Thread(this.OnTick);
        }

        public void Start() => this.ticker.Start();
        

        private void OnTick() {
            while (true){
                Thread.Sleep(Interval);
                // Time expired => notify clients
                Expired?.Invoke(DateTime.Now);
            }
        }
    }
}
