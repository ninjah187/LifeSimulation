using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;
using DotNetNinja.TypeFiltering;

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

        public bool IsClone
        {
            get { return _isClone; }
            set { SetProperty(ref _isClone, value); }
        }
        bool _isClone;

        public Organism(Point position, ICircleHitBox hitBox, IMover mover)
            : base(position, 20, hitBox)
        {
            Mover = mover;
            Energy = 50;
            IsObstacle = true;
        }

        public override void Update()
        {
            if (!Mover.Direction.Equals(default(Vector)))
            {
                Energy -= 0.1;
            }
        }

        public void Eat(IFood food)
        {
            food.Energy -= 0.2;
            food.Size -= 0.02 * 5;
            Energy += 0.5;
        }

        public IOrganism Clone()
        {
            Energy = 50;

            Func<IMover> moverFactory = null;
            Mover
                .When<Mover>(m => moverFactory = () => new Mover())
                .When<FoodTrackingMover>(m => moverFactory = () => new FoodTrackingMover())
                .ThrowIfNotRecognized();

            var clone = new Organism(Position, new CircleHitBox(), moverFactory());

            clone.Mover.CurrentStep = 0;
            clone.Mover.DirectionChangeStepsLimit = (int) Size;
            clone.Mover.Direction = -Mover.Direction;

            clone.IsClone = true;
            //clone.IsObstacle = false;

            return clone;
        }
    }
}
