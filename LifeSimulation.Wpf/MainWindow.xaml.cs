using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetNinja.TypeFiltering;
using LifeSimulation.Core;
using Environment = LifeSimulation.Core.Environment;

namespace LifeSimulation.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Func<IOrganism, Brush, OrganismControlBase> _organismControlFactory;

        public MainWindow()
        {
            InitializeComponent();

            _organismControlFactory = (organism, fill) => new OrganismControl(organism, fill);
            //_organismControlFactory = (organism, fill) => new OrganismControlDebug(organism, fill);

            Loaded += delegate
            {
                var environment = new Environment
                {
                    Width = GameCanvas.ActualWidth,
                    Height = GameCanvas.ActualHeight
                };

                var engine = new Engine(environment, this);

                engine.RunAsync();

                // bugged. gives NaN for both values. but idea is good.
                //environment
                //    .SetSizeBinding(nameof(Environment.Width), this, WidthProperty)
                //    .SetSizeBinding(nameof(Environment.Height), this, HeightProperty);
            };
        }

        public void AddObject(IGameObject gameObject)
        {
            gameObject
                .When<IOrganism>(o => AddOrganismToCanvas(o))
                .BreakIfRecognized()
                .When<IFood>(f => AddFoodToCanvas(f));
        }

        public void RemoveObject(IGameObject gameObject)
        {
            gameObject
                .When<IOrganism>(o => RemoveOrganismFromCanvas(o))
                .BreakIfRecognized()
                .When<IFood>(f => RemoveFoodFromCanvas(f));
        }

        public void AddOrganismToCanvas(IOrganism organism)
        {
            Brush fill = null;

            organism.Mover
                .When<Mover>(m =>
                {
                    fill = Brushes.Red;
                })
                .When<FoodTrackingMover>(m =>
                {
                    fill = Brushes.Blue;
                })
                .ThrowIfNotRecognized();

            GameCanvas.Children.Add(_organismControlFactory(organism, fill));
        }

        public void RemoveOrganismFromCanvas(IOrganism organism)
        {
            var control = GameCanvas.Children
                        .OfType<OrganismControl>()
                        .Single(c => c.Organism == organism);

            GameCanvas.Children.Remove(control);
        }

        public void AddFoodToCanvas(IFood food)
        {
            GameCanvas.Children.Add(new FoodControl(food));
        }

        public void RemoveFoodFromCanvas(IFood food)
        {
            var control = GameCanvas.Children
                        .OfType<FoodControl>()
                        .Single(c => c.Food == food);

            GameCanvas.Children.Remove(control);
        }
    }
}
