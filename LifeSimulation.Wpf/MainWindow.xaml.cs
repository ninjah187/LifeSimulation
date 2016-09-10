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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {
                var environment = new Environment
                {
                    Width = GameCanvas.ActualWidth,
                    Height = GameCanvas.ActualHeight
                };

                var engine = new Engine(environment);
                engine.AddObjectToGameCanvas = gameObject =>
                {
                    gameObject
                        .When<IOrganism>(o => AddOrganismToCanvas(o))
                        .BreakIfRecognized()
                        .When<IFood>(f => AddFoodToCanvas(f));
                };
                engine.AddOrganismToGameCanvas = AddOrganismToCanvas;
                engine.RemoveOrganismFromGameCanvas = RemoveOrganismFromCanvas;

                engine.RunAsync();

                // bugged. gives NaN for both values. but idea is good.
                //environment
                //    .SetSizeBinding(nameof(Environment.Width), this, WidthProperty)
                //    .SetSizeBinding(nameof(Environment.Height), this, HeightProperty);
            };
        }

        public void AddOrganismToCanvas(IOrganism organism)
        {
            GameCanvas.Children.Add(new OrganismControl(organism));
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
    }
}
