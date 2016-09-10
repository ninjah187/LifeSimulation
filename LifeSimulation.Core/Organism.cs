using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace LifeSimulation.Core
{
    public class Organism : CollidableGameObject, IOrganism
    {
        public IMover Mover
        {
            get { return _mover; }
            set { SetProperty(ref _mover, value); }
        }
        IMover _mover;

        public double Energy
        {
            get { return _energy; }
            set { SetProperty(ref _energy, value); }
        }
        double _energy;

        public Organism(Point position, ICircleHitBox hitBox, IMover mover)
            : base(position, 20, hitBox)
        {
            Mover = mover;
            Energy = 50;
        }

        public override void Update(params ICollidableGameObject[] nearby)
        {
            Mover.Move(this, nearby.OfType<IOrganism>().ToArray());

            foreach (var food in nearby.OfType<IFood>())
            {
                if (HitBox.Collides(food.HitBox))
                {
                    food.Energy -= 0.2;
                    food.Size -= 0.02 * 5;
                    Energy += 0.5;
                }
            }

            if (!Mover.Direction.Equals(default(Vector)))
            {
                Energy -= 0.1;
            }
        }
    }
}
