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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeSimulation.Wpf
{
    public class OrganismControlBase : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty OrganismProperty
            = DependencyProperty.Register(nameof(Organism), typeof(IOrganism), typeof(OrganismControl));

        public IOrganism Organism
        {
            get { return (IOrganism)GetValue(OrganismProperty); }
            set { SetValue(OrganismProperty, value); }
        }

        public OrganismControlBase(IOrganism organism)
        {
            Initialized += delegate
            {
                (Content as FrameworkElement).DataContext = this;

                Organism = organism;

                var binding = new Binding();
                binding.Source = Organism;
                binding.Path = new PropertyPath("Position.X");
                BindingOperations.SetBinding(this, Canvas.LeftProperty, binding);

                binding = new Binding();
                binding.Source = Organism;
                binding.Path = new PropertyPath("Position.Y");
                BindingOperations.SetBinding(this, Canvas.TopProperty, binding);
            };
        }

        protected static void OnDependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((OrganismControlBase)sender).PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(e.Property.Name));
        }
    }
}
