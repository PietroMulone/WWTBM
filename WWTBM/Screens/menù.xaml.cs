using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WWTBM.Screens
{
    /// <summary>
    /// Interaction logic for menù.xaml
    /// </summary>
    public partial class menù : UserControl
    {
        public static event EventHandler PlayOpened;
        public static event EventHandler gameSettings;
        public static event EventHandler gameCredits;
        public static event EventHandler gameExit;

        public menù()
        {
            InitializeComponent();
        }


        private void Open_Play(object sender, RoutedEventArgs e)
        {
            PlayOpened?.Invoke(this, EventArgs.Empty);
        }

        private void Settings_game(object sender, RoutedEventArgs e)
        {
            gameSettings?.Invoke(this, EventArgs.Empty);
        }
        private void Open_Credits(object sender, RoutedEventArgs e)
        {
            gameCredits?.Invoke(this, EventArgs.Empty);
        }

        private void Exit_Game(object sender, RoutedEventArgs e)
        {
            gameExit?.Invoke(this, EventArgs.Empty);
        }

    }
}
