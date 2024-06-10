using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWTBM.Screens.ViewModels
{
    public class SettingsViewModel : BaseModel
    {

        private int _Settings_Volume;


        public SettingsViewModel()
        {
            GetValues(ref _Settings_Volume);

        }

        public int Settings_Volume { get { return _Settings_Volume; } set { _Settings_Volume = value; OnPropertyChanged(nameof(Settings_Volume)); } }



        private void GetValues(ref int vol)     //leggo da settings i valori
        {
            int indice = 0;
            string riga;
            List<string> contenuto = new List<string>();
            using (StreamReader reader = new StreamReader("Data/Settings.txt"))
            {
                while ((riga = reader.ReadLine()) != null)
                {

                    if (riga.StartsWith("#"))
                    {
                        contenuto.Add(riga);
                        indice++;
                    }
                    else if (indice == 5)
                        vol = Convert.ToInt32(riga);

                }
            }
        }

    }
}
