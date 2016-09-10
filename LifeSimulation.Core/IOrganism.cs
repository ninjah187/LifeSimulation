using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IOrganism : IGameObject
    {
        IMover Mover { get; }
        double Energy { get; }

        void Update();
    }
}
