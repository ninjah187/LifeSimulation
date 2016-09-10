using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Food : CollidableGameObject, IFood
    {
        public double Energy
        {
            get { return _energy; }
            set { SetProperty(ref _energy, value); }
        }
        double _energy;

        public Food(Point position, ICircleHitBox hitBox)
            : base(position, 5, hitBox)
        {
            Energy = 10;
        }

        public override void Update()
        {
        }
    }
}
