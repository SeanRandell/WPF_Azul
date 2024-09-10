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
    public class ProductionLineClickCommand : CommandBase
    {
        private GameViewModel gameViewModel;
        public ProductionLineClickCommand(GameViewModel gameViewModel) : base()
        {
            this.gameViewModel = gameViewModel;
        }
        public override void Execute(object? parameter)
        {
            Trace.WriteLine("execute reached");
            // TODO - command for clicking on a production line after a player has picked a fsctory tile.
            if (parameter is not null)
            {
                ValidProductionTile clickedProductionTile = (ValidProductionTile)parameter;
                Trace.WriteLine("Test " + clickedProductionTile.ProductionTileIndex);
                gameViewModel.ProductionLineSelected(clickedProductionTile.ProductionTileIndex);
            }
        }
    }
}
