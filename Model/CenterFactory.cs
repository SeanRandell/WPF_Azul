﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPF_Azul.Model
{
    public class CenterFactory : Factory
    {
        public CenterFactory() : base(){
            Tile startingPlayerTile = new Tile(TileType.StartingPlayerMarker);
            startingPlayerTile.FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
        }

        public void ResetCenterFactory()
        {
            // TODO - depends on how we eant to get rid of player dropped tile.
        }

        public bool ContainsStartingPlayerMarker()
        {
            return factoryTiles.Any(x => x.TileType == TileType.StartingPlayerMarker);
        }
    }
}
