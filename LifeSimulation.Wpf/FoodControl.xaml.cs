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
    /// <summary>
    /// Interaction logic for FoodControl.xaml
    /// </summary>
    public partial class FoodControl : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty FoodProperty
            = DependencyProperty.Register(nameof(Food), typeof(IFood), typeof(FoodControl));

        public IFood Food
        {
            get { return (IFood)GetValue(FoodProperty); }
            set { SetValue(FoodProperty, value); }
        }

        public FoodControl()
            : this(null)
        {
        }

        public FoodControl(IFood food)
        {
            InitializeComponent();

            (Content as FrameworkElement).DataContext = this;

            Food = food;

            SetValue(Canvas.TopProperty, food.Position.Y);
            SetValue(Canvas.LeftProperty, food.Position.X);

            //var binding = new Binding();
            //binding.Source = Food;
            //binding.Path = new PropertyPath("Position.X");
            //BindingOperations.SetBinding(this, Canvas.LeftProperty, binding);

            //binding = new Binding();
            //binding.Source = Food;
            //binding.Path = new PropertyPath("Position.Y");
            //BindingOperations.SetBinding(this, Canvas.TopProperty, binding);
        }

        protected static void OnDependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((FoodControl)sender).PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(e.Property.Name));
        }
    }
}
