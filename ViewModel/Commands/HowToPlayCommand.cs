using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.ViewModel.Commands
{
    internal class HowToPlayCommand : CommandBase
    {
        private GameViewModel _gameViewModel;

        public HowToPlayCommand(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        public override void Execute(object? parameter)
        {
            _gameViewModel.ShowHowToPlayWindow();
        }
    }
}
