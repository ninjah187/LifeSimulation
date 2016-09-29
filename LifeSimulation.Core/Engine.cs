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
        static Random Random { get; } = new Random();

        public Action<IGameObject> AddObjectToGameCanvas { get; set; }
        public Action<IGameObject> RemoveObjectFromGameCanvas { get; set; }
        public Action<IOrganism> AddOrganismToGameCanvas { get; set; }
        public Action<IOrganism> RemoveOrganismFromGameCanvas { get; set; }

        //List<IOrganism> _organisms;
        List<IGameObject> _objects;

        IEnvironment _environment;
        IMapCollisionDetector _mapCollisionDetector;

        ICollider _collider;

        public Engine(IEnvironment environment)
        {
            _environment = environment;
            _mapCollisionDetector = new MapCollisionDetector(_environment);

            _collider = new Collider();

            _objects = new List<IGameObject>
            {
                //new Organism(_environment.Center, new CircleHitBox(), new Mover(_mapCollisionDetector)),
                new Organism(new Point(100, 100), new CircleHitBox(), new Mover(_mapCollisionDetector)),
                new Organism(
                    new Point
                    {
                        X = _environment.Width - 100,
                        Y = _environment.Height - 100
                    },
                    new CircleHitBox(), 
                    new FoodTrackingMover(_mapCollisionDetector))
            };

            SpawnFood(400);
        }

        List<Food> SpawnFood(int count)
        {
            var spawnedFood = new List<Food>();

            for (int i = 0; i < count; i++)
            {
                var food = new Food(new Point
                {
                    X = Random.Next(5, (int)_environment.Width - 5),
                    Y = Random.Next(5, (int)_environment.Height - 5)
                }, new CircleHitBox());

                var add = true;

                foreach (var organism in _objects.OfType<IOrganism>())
                {
                    if (organism.HitBox.Collides(food.HitBox))
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                {
                    _objects.Add(food);
                    spawnedFood.Add(food);
                }
            }

            return spawnedFood;
        }

        public void Update()
        {
            var dying = new List<IOrganism>();
            var vanishingFood = new List<IFood>();

            var _organisms = _objects.OfType<IOrganism>();

            foreach (var organism in _organisms)
            {
                if (organism.Energy <= 0)
                {
                    dying.Add(organism);
                }
            }

            foreach (var food in _objects.OfType<IFood>())
            {
                if (food.Energy <= 0)
                {
                    vanishingFood.Add(food);
                }
            }

            foreach (var organism in dying)
            {
                //_organisms.Remove(organism);
                _objects.Remove(organism);
                RemoveOrganismFromGameCanvas(organism);
            }

            foreach (var food in vanishingFood)
            {
                _objects.Remove(food);
                RemoveObjectFromGameCanvas(food);
            }

            var collidableGameObjects = _objects.OfType<ICollidableGameObject>().ToList();

            foreach (var obj in collidableGameObjects)
            {
                obj.Update(collidableGameObjects.Where(c => obj != c).ToArray());
            }

            var cloned = new List<IOrganism>();

            foreach (var organism in _organisms.Where(o => o.Energy >= 100))
            {
                var clone = organism.Clone();
                cloned.Add(clone);
                AddObjectToGameCanvas(clone);
            }

            _objects.AddRange(cloned);

            foreach (var obj in _objects)
            {
                obj.Update();
            }
        }

        public async Task RunAsync()
        {
            foreach (var obj in _objects)
            {
                AddObjectToGameCanvas(obj);
            }

            int stepCount = 0;

            while (true)
            {
                if (stepCount >= 100)
                {
                    var spawned = SpawnFood(10);
                    spawned.ForEach(f => AddObjectToGameCanvas(f));
                    stepCount = 0;
                }

                Update();
                await Task.Delay(16);

                stepCount++;
            }
        }
    }
}
