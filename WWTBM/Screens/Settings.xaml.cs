using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WWTBM.DataStructure;
using WWTBM.Screens.ViewModels;

namespace WWTBM.Screens
{
    public delegate void SettingsSaveEventHandler(object sender, settings_args e);

    public partial class Settings : UserControl                 //TODO: Crea il viewModel, importalo e utilizzalo
    {
        private SettingsViewModel _viewModel;

        public static event EventHandler ClickUndo;
        public static event SettingsSaveEventHandler Save_Settings;

        public Settings()
        {
            _viewModel = new SettingsViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            ClickUndo?.Invoke(this, EventArgs.Empty);
        }

            private void Settings_Save(object sender, RoutedEventArgs e)
        {
            settings_args args = new settings_args();
            args.Audio_Volume = _viewModel.Settings_Volume;
            args.Question_Number = 0;                //<-->  spero che il viewModel risolva tutto

            Save_Settings?.Invoke(this, args);
        }

        private void ResetQuestions(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sei Sicuro di voler eliminare i record delle domande?", "Conferma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string percorso = "Data/BlackList.txt";
                using (StreamWriter writer = new StreamWriter(percorso))
                {
                    writer.Write("");
                }

            }
        }
    }
}
