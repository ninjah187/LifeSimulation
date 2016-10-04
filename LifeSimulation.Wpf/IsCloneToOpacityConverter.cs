using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LifeSimulation.Core;

namespace LifeSimulation.Wpf
{
    public class IsCloneToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isClone = (bool) value;

            if (isClone)
            {
                return 0.5;
            }
            else
            {
                return 0.8;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
