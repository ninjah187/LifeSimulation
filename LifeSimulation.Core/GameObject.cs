using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace LifeSimulation.Core
{
    public class GameObject : PropertyChangedNotifier, IGameObject
    {
        public Guid Id { get; }

        public Point Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }
        Point _position;

        public double Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }
        double _size;

        public GameObject(Point position, double size)
        {
            Id = Guid.NewGuid();
            Position = position;
            Size = size;
        }

        public virtual void Update()
        {
        }
    }
}
