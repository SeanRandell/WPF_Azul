using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.ViewModel.Commands
{
    internal class UndoFactoryTileClick : CommandBase
    {
        private GameViewModel _gameViewModel;
        public UndoFactoryTileClick(GameViewModel gameViewModel) : base()
        {
            _gameViewModel = gameViewModel;
        }

        public override void Execute(object? parameter)
        {
            _gameViewModel.UndoFactoryTileSelected();
        }
    }
}
