using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFapp.WPFLibrary.Commands;
using static WWTBM.Screens.ViewModels.Commands.Risposta4;

namespace WWTBM.Screens.ViewModels.Commands
{
    public class Risposta1 : CommandBase
    {
        private GameViewModel _ViewModel;
        public Risposta1(GameViewModel mainViewModel)
        {
            _ViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }
        public override void Execute(object parameter)
        {
            GameControll.Controll(1,  _ViewModel);
        }
    }

    public class Risposta2 : CommandBase
    {
        private GameViewModel _ViewModel;
        public Risposta2(GameViewModel mainViewModel)
        {
            _ViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }
        public override void Execute(object parameter)
        {
            GameControll.Controll(2,  _ViewModel);
        }
    }

    public class Risposta3 : CommandBase
    {
        private GameViewModel _ViewModel;
        public Risposta3(GameViewModel mainViewModel)
        {
            _ViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }
        public override void Execute(object parameter)
        {
            GameControll.Controll(3,  _ViewModel);
        }
    }

    public class Risposta4 : CommandBase
    {
        private GameViewModel _ViewModel;
        public Risposta4(GameViewModel mainViewModel)
        {
            _ViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }
        public override void Execute(object parameter)
        {
            GameControll.Controll(4,  _ViewModel);
        }


        internal static class GameControll
        {
            internal static void Controll(byte pulsante,  GameViewModel _ViewModel)
            {
                //souno
                _ViewModel.player.Open(new Uri("Audio/FinalAnswer.mp3", UriKind.RelativeOrAbsolute));
                _ViewModel.player.Play();
                if (_ViewModel.GameStatus == 2)
                {
                    _ViewModel.QuestionImage = _ViewModel.QImagePath + "g" + pulsante + ".png";
                    _ViewModel.Choice = pulsante;
                }
               
            }
        }
    }
}
