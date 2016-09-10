using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LifeSimulation.Core;

namespace LifeSimulation.Wpf
{
    public static class EnvironmentExtensions
    {
        public static IEnvironment SetSizeBinding(this IEnvironment environment, string propertyName, 
                                                  DependencyObject depObject, DependencyProperty dp)
        {
            var binding = new Binding();
            binding.Source = environment;
            binding.Path = new PropertyPath(propertyName);
            binding.Mode = BindingMode.OneWayToSource;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(depObject, dp, binding);

            return environment;
        }
    }
}
