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
using LifeSimulation.Core;

namespace LifeSimulation.Wpf
{
    /// <summary>
    /// Interaction logic for OrganismControlDebug.xaml
    /// </summary>
    public partial class OrganismControlDebug : OrganismControlBase
    {
        public OrganismControlDebug(IOrganism organism, Brush fill)
            : base(organism)
        {
            InitializeComponent();

            MainEllipse.Fill = fill;
        }
    }
}
