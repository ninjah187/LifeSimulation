using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace LifeSimulation.Core
{
    public class Organism : GameObject, IOrganism
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

        public Organism(Point position, IMover mover)
            : base(position, 20)
        {
            Mover = mover;
            Energy = 100;
        }

        public void Update()
        {
            Mover.Move(this);

            if (!Mover.Direction.Equals(default(Vector)))
            {
                Energy -= 0.1;
            }
        }
    }
}
