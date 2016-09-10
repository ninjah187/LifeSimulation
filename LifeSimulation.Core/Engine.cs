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
        public Action<IOrganism> AddOrganismToGameCanvas { get; set; }
        public Action<IOrganism> RemoveOrganismFromGameCanvas { get; set; }

        //List<IOrganism> _organisms;
        List<IGameObject> _objects;

        IEnvironment _environment;
        IMapCollisionDetector _mapCollisionDetector;

        public Engine(IEnvironment environment)
        {
            _environment = environment;
            _mapCollisionDetector = new MapCollisionDetector(_environment);

            _objects = new List<IGameObject>
            {
                new Organism(_environment.Center, new RandomMover(new CircleHitBox(), _mapCollisionDetector))
            };

            for (int i = 0; i < 100; i++)
            {
                _objects.Add(new Food(new Point
                {
                    X = Random.Next(5, (int) _environment.Width - 5),
                    Y = Random.Next(5, (int) _environment.Height - 5)
                }));
            }

            //_organisms = new List<IOrganism>
            //{
            //    new Organism(_environment.Center, new RandomMover(new CircleHitBox(), _mapCollisionDetector))
            //};
        }
        
        public void Update()
        {
            var dying = new List<IOrganism>();

            var _organisms = _objects.OfType<IOrganism>();

            foreach (var organism in _organisms)
            {
                if (organism.Energy <= 0)
                {
                    dying.Add(organism);
                    continue;
                }

                organism.Update();
            }

            foreach (var organism in dying)
            {
                //_organisms.Remove(organism);
                _objects.Remove(organism);
                RemoveOrganismFromGameCanvas(organism);
            }
        }

        public async Task RunAsync()
        {
            //foreach (var organism in _organisms)
            //{
            //    AddOrganismToGameCanvas(organism);
            //}

            foreach (var obj in _objects)
            {
                AddObjectToGameCanvas(obj);
            }

            while (true)
            {
                Update();
                await Task.Delay(16);
            }
        }
    }
}
