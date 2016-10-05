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
        
        List<IGameObject> _objects;

        IEnvironment _environment;
        ICollisionEngine _collisionEngine;

        public Engine(IEnvironment environment)
        {
            _environment = environment;
            _collisionEngine = new CollisionEngine(new MapCollisionDetector(_environment));

            _objects = new List<IGameObject>
            {
                //new Organism(_environment.Center, new CircleHitBox(), new Mover()),
                new Organism(new Point(100, 100), new CircleHitBox(), new Mover()),
                new Organism(
                    new Point
                    {
                        X = _environment.Width - 100,
                        Y = _environment.Height - 100
                    },
                    new CircleHitBox(),
                    new FoodTrackingMover())
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

            var organisms = _objects.OfType<IOrganism>();

            foreach (var organism in organisms)
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
                _objects.Remove(organism);
                RemoveOrganismFromGameCanvas(organism);
            }

            foreach (var food in vanishingFood)
            {
                _objects.Remove(food);
                RemoveObjectFromGameCanvas(food);
            }

            var cloned = new List<IOrganism>();
            foreach (var organism in organisms.Where(o => o.Energy >= 100))
            {
                var clone = organism.Clone();
                cloned.Add(clone);
                AddObjectToGameCanvas(clone);
            }

            _objects.AddRange(cloned);

            var collisionEngineRunSummary = _collisionEngine.Run(_objects.ToArray());

            var clonesWithoutCollisionsWithOrganisms = _objects
                .OfType<IOrganism>()
                .Where(o => o.IsClone && 
                            (!collisionEngineRunSummary.Collisions.ContainsKey(o) || 
                             !collisionEngineRunSummary.Collisions[o].Any(c => c is IOrganism)));

            foreach (var clone in clonesWithoutCollisionsWithOrganisms)
            {
                clone.IsClone = false;
            }

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
