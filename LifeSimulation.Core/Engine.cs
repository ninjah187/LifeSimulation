using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LifeSimulation.Core
{
    public class Engine : IEngine
    {
        protected IMover[] Movers { get; }

        public Engine(params IMover[] movers)
        {
            Movers = movers;
        }
        
        public void Update()
        {
            foreach (var mover in Movers)
            {
                mover.Move();
            }
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Update();
                await Task.Delay(16);
            }
        }
    }
}
