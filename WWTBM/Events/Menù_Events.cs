using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WWTBM.DataStructure;
using WWTBM.Screens;

namespace WWTBM
{
    public partial class MainWindow : Window
    {
        private void menù_PlayOpened(object sender, EventArgs e)     //dal menù premi play
        {
            CurrentContent = LoadScreen("GameView.xaml");
        }

        private void menù_gameSettings(object sender, EventArgs e)  //dal menù premi Settings
        {
            CurrentContent = LoadScreen("Settings.xaml");
        }

        private void menù_Credits(object sender, EventArgs e)     //dal menù premi play
        {
            CurrentContent = LoadScreen("Credits.xaml");
        }

        private void menù_Exit(object sender, EventArgs e)     //dal menù premi play
        {
            MessageBoxResult result = MessageBox.Show("Sei sicuro di voler uscire?", "Conferma uscita", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Credits_ClickBack(object sender, EventArgs e)
        {
            CurrentContent = LoadScreen("menù.xaml");
        }


        #region SETTINGS
        private void Settings_Undo(object sender, EventArgs e)
        {
            CurrentContent = LoadScreen("menù.xaml");
        }

        private void Save_Settings(object sender, settings_args e)
        {

            int indice = 0;
            string riga;
            List<string> contenuto = new List<string>();


            //Leggo e trovo l'indice della riga
           
            using (StreamReader reader = new StreamReader("Data/Settings.txt"))
            {
                while ((riga = reader.ReadLine()) != null)
                {
                    
                    if (riga.StartsWith("#"))
                    {
                        contenuto.Add(riga);
                        indice++;
                    }
                        
                }
            }
            //Scrivo
            using (StreamWriter writer = new StreamWriter("Data/Settings.txt"))
            {
                foreach (string line in contenuto)
                {
                    writer.Write(line +"\n");
                }
                
                writer.Write(e.Question_Number+"\n");
                writer.Write(e.Audio_Volume); 
            }

            CurrentContent = LoadScreen("menù.xaml");
        }
        #endregion


    }
}
