using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates {
    public class TimerClient {
        public static void Test() {
            Timer timer = new Timer();
            timer.Interval = 500;

            // Register with concrete method
            timer.Expired += TimerExpired; 
            
            // Register with anonymous method
            timer.Expired += delegate(DateTime signaledTime) {
                Console.WriteLine($"Expired at (anomymous): {signaledTime:HH:mm:ss}");
            };

            //Register with Lambda
            timer.Expired += (DateTime signaledTime) => Console.WriteLine($"Expired at (Lambda): {signaledTime:HH:mm:ss}");

            timer.Start();
        }

        private static void TimerExpired(DateTime signaledTime) {
            Console.WriteLine($"Expired at (concrete): {signaledTime:HH:mm:ss}");
        }
    }
}
