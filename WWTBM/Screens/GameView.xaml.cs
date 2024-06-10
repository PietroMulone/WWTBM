using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;
using WWTBM.Screens.ViewModels;

namespace WWTBM.Screens
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        Random random = new Random();
        private GameViewModel _gameViewModel;
        public static event EventHandler EventoGameToMenù;
        private MediaPlayer player;
        private DispatcherTimer timer;
        private TimeSpan pausePosition;


        public GameView()
        {
            _gameViewModel = new GameViewModel();
            _gameViewModel.AudioRefresh += ViewModel_AudioRefresh;

            InitializeComponent();
            Focusable = true;
            Loaded += (sender, e) => Focus();

            DataContext = _gameViewModel;


            player = new MediaPlayer();

            player.MediaOpened += Player_MediaOpened;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

        }



            private void GameToMenù(object sender, RoutedEventArgs e)
        {
            player.Pause();
            player.Position = TimeSpan.Zero;
            _gameViewModel.player.Pause();
            EventoGameToMenù?.Invoke(this, EventArgs.Empty);
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                if (player.Position != TimeSpan.Zero && player.NaturalDuration.HasTimeSpan)
                {
                    // Il player è in riproduzione, quindi mettilo in pausa
                    if (player.CanPause)
                    {
                        player.Pause();
                        player.Position = TimeSpan.Zero;
                    }
                }
                else
                {
                    string percorso = "Audio/bed (" + random.Next(1, 11) + ").mp3";
                    player.Open(new Uri(percorso, UriKind.RelativeOrAbsolute));
                    player.Play();
                }
                
            }

            if (e.Key == Key.Enter && _gameViewModel.GameStatus == 0)
            {
                _gameViewModel.GameStatus = 1;
            }
            else if (e.Key == Key.Enter && _gameViewModel.GameStatus == 1)
            {
                _gameViewModel.GameStatus = 2;
            }
           
        else if (e.Key == Key.Enter && _gameViewModel.Choice != 0 && _gameViewModel.GameStatus == 2)
            {
                _gameViewModel.GameStatus = 3;
            }
            else if (e.Key == Key.Enter && _gameViewModel.GameStatus == 3)
            {
                _gameViewModel.GameStatus = 0;
            }
        }


        private void ViewModel_AudioRefresh(object sender, EventArgs e)
        {
            ResfreshButtonPressUp(null, null);
        }



        private void RefreshButtonPressDown(object sender, MouseButtonEventArgs e)
        {
            RefreshButton.Source = new BitmapImage(new Uri("../Images/refresh2.png", UriKind.Relative));
            
        }

        private void ResfreshButtonPressUp(object sender, MouseButtonEventArgs e)
        {
            RefreshButton.Source = new BitmapImage(new Uri("../Images/refresh1.png", UriKind.Relative));
            player.Position = TimeSpan.Zero;
            pausePosition = TimeSpan.Zero;
            SoundProgressBar.Value = player.Position.TotalSeconds;

        }

        private void playButtonPressDown(object sender, MouseButtonEventArgs e)
        {
            PlayButton.Source = new BitmapImage(new Uri("../Images/play2.png", UriKind.Relative));
        }
        private void playButtonPressUp(object sender, MouseButtonEventArgs e)
        {
            PlayButton.Source = new BitmapImage(new Uri("../Images/play1.png", UriKind.Relative));
            _gameViewModel.PlayVisible = "Hidden";
            _gameViewModel.PauseVisible = "Visible";

            string link = "Data\\AUDIOQuestions\\" + _gameViewModel.SongQuestionFileName;
            
            if (pausePosition != TimeSpan.Zero)
            {
                player.Position = pausePosition;
                pausePosition = TimeSpan.Zero;
                player.Play();
                timer.Start();
            }
            else
            {
                player.Open(new Uri(link, UriKind.RelativeOrAbsolute));  
                player.Play();
                timer.Start();
            }
        }


        private void pauseButtonPressDown(object sender, MouseButtonEventArgs e)
        {
        }
        private void pauseButtonPressUp(object sender, MouseButtonEventArgs e)
        {
            PauseButton.Source = new BitmapImage(new Uri("../Images/pause1.png", UriKind.Relative));
            //PauseButton.Visibility = Visibility.Hidden;
            _gameViewModel.PlayVisible = "Visible";
            //PlayButton.Visibility = Visibility.Visible;

            pausePosition = player.Position;
            player.Pause();
            timer.Stop();
        }




      
        private void Player_MediaOpened(object sender, EventArgs e)
        {
            SoundProgressBar.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            SoundProgressBar.Value = player.Position.TotalSeconds;
        }

    }


}
