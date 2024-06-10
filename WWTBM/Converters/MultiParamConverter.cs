using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WWTBM.Converters
{
    public class MultiParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Esegui qui la tua logica di conversione utilizzando i parametri passati
            // Nel nostro esempio, supponiamo di avere i seguenti tipi di parametri:
            // values[0]: double (ActualWidth)
            // values[1]: string (Parametro aggiuntivo)

            double cellWidth = (double)values[0];
            string testo = values[1] as string;
            string tipo = values[2] as string;
            int wordCount = testo.Split(' ').Length;
            double fontSize;


            if (testo != null && tipo == "Question")
            {
                fontSize = cellWidth / 40; // Puoi regolare il fattore di scala a tuo piacimento
                if (wordCount > 18)
                    fontSize = fontSize * 0.7;
                if (fontSize <= 0)
                    fontSize = 1;
                return fontSize;
            }
            else if (testo != null && tipo == "answer")
            {
                fontSize = cellWidth / 70; // Puoi regolare il fattore di scala a tuo piacimento
                if (wordCount > 18)
                    fontSize = fontSize * 0.8;
                if (fontSize <= 0)
                    fontSize = 1;
                return fontSize;
            }

            fontSize = cellWidth / 60; // Puoi regolare il fattore di scala a tuo piacimento
            if (fontSize <= 0)
                fontSize = 1;
            return fontSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // Implementazione opzionale, se non è necessario il two-way binding, è possibile lanciare NotImplementedException()
            throw new NotImplementedException();
        }
    }
}
