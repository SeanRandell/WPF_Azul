using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.ViewModel.Commands
{
    internal class ReplayGameCommand : CommandBase
    {
        private GameViewModel _gameViewModel;

        public ReplayGameCommand(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        public override void Execute(object? parameter)
        {
            _gameViewModel.ResetGame();
        }
    }
}
