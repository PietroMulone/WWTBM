using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using WWTBM.Screens;


namespace WWTBM
{
    public partial class MainWindow : Window
    {
        



        public static readonly DependencyProperty CurrentContentProperty = DependencyProperty.Register(
            nameof(CurrentContent),
            typeof(object),
            typeof(MainWindow),
            new PropertyMetadata(default));

        public object CurrentContent
        {
            get => GetValue(CurrentContentProperty);
            set => SetValue(CurrentContentProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            // Imposta il contenuto iniziale
            CurrentContent = LoadScreen("menù.xaml");
            menù.PlayOpened += menù_PlayOpened;
            menù.gameSettings += menù_gameSettings;
            menù.gameCredits += menù_Credits;
            menù.gameExit += menù_Exit;
            Settings.ClickUndo += Settings_Undo;
            Settings.Save_Settings += Save_Settings;
            Credits.CreditsClickBack += Credits_ClickBack;
            GameView.EventoGameToMenù += GameView_EventoGameToMenù;
        }

        

        public object LoadScreen(string screenName)
        {
            var uri = new Uri($"/WWTBM;component/Screens/{screenName}", UriKind.Relative);
            var resource = Application.LoadComponent(uri);
            return resource;
        }

        // Metodo per cambiare il contenuto del ContentControl
        public void ChangeContent(string screenName)
        {
            CurrentContent = LoadScreen(screenName);
            
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Imposta la larghezza e l'altezza uguali per mantenere le proporzioni
            if (e.NewSize.Width != e.PreviousSize.Width)
            {
                this.Height = this.Width*0.6;
            }
            else if (e.NewSize.Height != e.PreviousSize.Height)
            {
                this.Width = this.Height/0.6;
            }
        }




    }

}
