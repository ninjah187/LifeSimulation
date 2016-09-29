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

        public IMapCollisionDetector MapCollisionDetector { get; set; }

        public int CurrentStep { get; set; }
        public int DirectionChangeStepsLimit { get; set; }

        public Mover(IMapCollisionDetector mapCollisionDetector)
        {
            MapCollisionDetector = mapCollisionDetector;
        }

        public void ApplyForce(Vector force)
        {
        }

        public void Move(ICollidableGameObject gameObject, params ICollidableGameObject[] nearby)
        {
            var obstacles = nearby.OfType<IOrganism>().ToArray();

            if (CurrentStep >= DirectionChangeStepsLimit)
            {
                ChangeDirection(gameObject, nearby);
            }

            CurrentStep++;

            var newPosition = gameObject.Position + Direction;

            gameObject.HitBox.Update(newPosition, gameObject.Size);

            var canPass = true;

            do
            {
                canPass = !MapCollisionDetector.Collides(gameObject.HitBox);

                canPass &= !CollidesWithObstacles(gameObject, obstacles);

                if (!canPass)
                {
                    ChangeDirection(gameObject, nearby);
                    newPosition = gameObject.Position + Direction;
                    gameObject.HitBox.Update(newPosition, gameObject.Size);
                }
            } while (!canPass);

            gameObject.Position = newPosition;
        }

        bool CollidesWithObstacles(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> obstacles)
        {
            var isClone = (gameObject as IOrganism)?.IsClone ?? false;
            var clonesCollided = false;

            foreach (var obj in obstacles)
            {
                if (gameObject.HitBox.Collides(obj.HitBox))
                {
                    var objIsClone = (obj as IOrganism)?.IsClone ?? false;
                    if (isClone || objIsClone)
                    {
                        clonesCollided = true;
                        continue;
                    }
                    return true;
                }
            }

            if (isClone && !clonesCollided)
            {
                ((IOrganism) gameObject).IsClone = false;
            }

            return false;
        }

        protected virtual void ChangeDirection(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> nearby)
        {
            CurrentStep = 0;
            DirectionChangeStepsLimit = Random.Next(5, 21);

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
