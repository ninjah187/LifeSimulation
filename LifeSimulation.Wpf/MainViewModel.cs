using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;
using LifeSimulation.Core;

namespace LifeSimulation.Wpf
{
    public class MainViewModel : PropertyChangedNotifier
    {
        public IMover Mover
        {
            get { return _mover; }
            set { SetProperty(ref _mover, value); }
        }
        IMover _mover;

        public IMover Mover2
        {
            get { return _mover2; }
            set { SetProperty(ref _mover2, value); }
        }
        IMover _mover2;

        public IEngine Engine { get; protected set; }

        public IEnvironment Environment { get; protected set; }

        public MainViewModel(IEnvironment environment)
        {
            Environment = environment;

            var mapCollisionDetector = new MapCollisionDetector(environment);

            Mover = new RandomMover(Environment.Center, new CircleHitBox(), mapCollisionDetector)
            {
                Size = 20
            };
            Mover2 = new RandomMover(Environment.Center, new CircleHitBox(), mapCollisionDetector)
            {
                Size = 20
            };
            Engine = new Engine(Mover, Mover2);

            Engine.RunAsync();
        }
    }
}
