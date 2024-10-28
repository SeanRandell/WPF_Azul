using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    internal class Factory : FactoryBase
    {
        public Factory(int factoryIndex) : base(factoryIndex)
        {
            FactoryTiles = new List<Tile>();
        }

        internal void SetupFactory(TileCollections tileCollections)
        {
            for (int i = 0; i < GameConstants.NORMAL_FACTORY_MAX_TILES; i++)
            {
                Tile currentTile = tileCollections.GetRandomTileFromBag();
                currentTile.FactoriesIndex = FactoryIndex;
                AddFactoryTile(currentTile);
            }
        }

        internal List<Tile> RemoveRemainingTiles()
        {
            List<Tile> remainingTiles = FactoryTiles.ToList();
            foreach (Tile tile in remainingTiles)
            {
                tile.FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
            }
            FactoryTiles.Clear();
            Trace.WriteLine("remaining tile count after removing factory tiles = " + remainingTiles.Count);
            return remainingTiles;
        }
    }
}
