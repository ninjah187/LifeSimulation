using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IOrganism : ICollidableGameObject, IMovingGameObject
    {
        double Energy { get; set; }
        bool IsClone { get; set; }
        IOrganism Clone();
        void Eat(IFood food);
    }
}
