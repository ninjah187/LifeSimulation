using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.TypeFiltering;
using DotNetNinja.NotifyPropertyChanged;

namespace LifeSimulation.Core
{
    public class Mover : PropertyChangedNotifier, IMover
    {
        protected static Random Random { get; } = new Random();

        public Vector Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        Vector _direction;

        IMapCollisionDetector _mapCollisionDetector;

        int _currentStep;
        int _directionChangeStepsLimit;

        public Mover(IMapCollisionDetector mapCollisionDetector)
        {
            _mapCollisionDetector = mapCollisionDetector;
        }

        public void ApplyForce(Vector force)
        {
        }

        public void Move(ICollidableGameObject gameObject, params ICollidableGameObject[] obstacles)
        {
            if (_currentStep >= _directionChangeStepsLimit)
            {
                ChangeDirection();
            }

            _currentStep++;

            var newPosition = gameObject.Position + Direction;

            gameObject.HitBox.Update(newPosition, gameObject.Size);

            var canPass = true;

            do
            {
                canPass = !_mapCollisionDetector.Collides(gameObject.HitBox);

                canPass &= !CollidesWithObstacles(gameObject, obstacles);

                if (!canPass)
                {
                    ChangeDirection();
                    newPosition = gameObject.Position + Direction;
                    gameObject.HitBox.Update(newPosition, gameObject.Size);
                }
            } while (!canPass);

            gameObject.Position = newPosition;
        }

        bool CollidesWithObstacles(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> obstacles)
        {
            foreach (var obj in obstacles)
            {
                if (gameObject.HitBox.Collides(obj.HitBox))
                {
                    return true;
                }
            }

            return false;
        }

        void ChangeDirection()
        {
            _currentStep = 0;
            _directionChangeStepsLimit = Random.Next(5, 21);

            //var oldMin = 0;
            //var oldMax = 1;
            //var newMin = -1;
            //var newMax = 1;

            Direction = new Vector
            {
                X = Random.Next(-1, 2),
                Y = Random.Next(-1, 2)
                //X = (((Random.NextDouble() - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin,
                //Y = (((Random.NextDouble() - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin
            };
        }
    }
}
