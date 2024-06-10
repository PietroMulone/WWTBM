using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Security.Cryptography;

namespace WWTBM.Converters
{
    public class ButtonSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double NewWidth;
            if (value is double cellWidth && cellWidth > 0)
            {
                if(parameter is string text)
                {
                    if (text != null && text == "littleButton")    
                {
                        NewWidth = cellWidth / 15; // Puoi regolare il fattore di scala a tuo piacimento
                        return NewWidth;
                    }
                }
                
                

                NewWidth = cellWidth /8; // Puoi regolare il fattore di scala a tuo piacimento
                return NewWidth;
            }
            return DependencyProperty.UnsetValue;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
