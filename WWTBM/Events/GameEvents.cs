using System;
using System.Windows;

namespace WWTBM
{
    public partial class MainWindow : Window
    {
        private void GameView_EventoGameToMenù(object sender, EventArgs e)
        {
            CurrentContent = LoadScreen("menù.xaml");
        }
    }
}
