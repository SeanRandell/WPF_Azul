using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;
using WPF_Azul.ViewModel;

namespace WPF_Azul.Commands
{
    public class FactoryTileClickCommand : CommandBase
    {
        private GameManager gameManager;
        private GameViewModel gameViewModel;

        public FactoryTileClickCommand(GameManager gameManager, GameViewModel gameViewModel) : base()
        {
            this.gameManager = gameManager;
            this.gameViewModel = gameViewModel;
        }
        public override void Execute(object? parameter)
        {

            // TODO - command for a player selecting a factory tile and placing a preview of these tiles in a selected tiles window.
            // TODO - add selected tiles window.
            if (parameter is not null)
            {
                Tile selectedFactoryTile = (Tile)parameter;
                Trace.WriteLine(selectedFactoryTile.TileType);
                Trace.WriteLine("Factory Index Clicked " + selectedFactoryTile.FactoriesIndex);
                gameViewModel.FactoryTileSelected(selectedFactoryTile.TileType, selectedFactoryTile.FactoriesIndex);
            }

            //Trace.WriteLine("text");
        }
    }
}
