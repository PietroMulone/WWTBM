using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WWTBM.Converters
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double cellWidth && cellWidth > 0)
            {
                string text = parameter as string;
                int wordCount = 0;

                double fontSize;
                if (text != null && text == "TITLE")
                {
                    fontSize = cellWidth / 25; // Puoi regolare il fattore di scala a tuo piacimento
                    return fontSize;
                }
                else if (text != null && text ==  "Question")
                {
                    fontSize = cellWidth / 40; // Puoi regolare il fattore di scala a tuo piacimento
                    if (wordCount > 18)
                        fontSize = fontSize * 0.7;
                    return fontSize;
                }
                else if (text != null && text == "answers")
                {
                    fontSize = cellWidth / 70; // Puoi regolare il fattore di scala a tuo piacimento
                    if (wordCount > 18)
                        fontSize = fontSize * 0.7;
                    return fontSize;
                }

                fontSize = cellWidth / 60; // Puoi regolare il fattore di scala a tuo piacimento
                return fontSize;
            }
            return DependencyProperty.UnsetValue;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
