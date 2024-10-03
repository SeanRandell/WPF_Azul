using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class Factory
    {
        protected int factoryIndex;

        public int FactoryIndex
        {
            get { return factoryIndex; }
            set { factoryIndex = value; }
        }

        protected List<Tile> factoryTiles;

        public List<Tile> FactoryTiles
        {
            get { return factoryTiles; }
            set { factoryTiles = value; }
        }

        public Factory()
        {
            factoryTiles = new List<Tile>();
        }

        public void SetupFactory(TileCollections tileCollections)
        {
            for (int i = 0; i < GameConstants.NORMAL_FACTORY_MAX_TILES; i++)
            {
                Tile currentTile = tileCollections.GetRandomTileFromBag();
                currentTile.FactoriesIndex = factoryIndex;
                AddFactoryTile(currentTile);
            }
        }

        public int AddFactoryTile(Tile tile) 
        {
            int preAdditionIndex = factoryTiles.Count;
            factoryTiles.Add(tile);
            return preAdditionIndex;
        }

        internal void AddTiles(List<Tile> tiles)
        {
            factoryTiles.AddRange(tiles);
        }

        public List<Tile> TakeAllTilesOfType(TileType currentTileType)
        {
            return factoryTiles.Where(t => t.TileType == currentTileType).ToList();
        }

        public void ProcessFactoryTilesSelectedForProduction(TileType currentTileType)
        {
            factoryTiles.RemoveAll(t => t.TileType == currentTileType);
        }

        internal List<Tile> RemoveRemainingTiles()
        {
            List<Tile> remainingTiles = factoryTiles.ToList();
            foreach (Tile tile in remainingTiles)
            {
                tile.FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
            }
            factoryTiles.Clear();
            Trace.WriteLine("remaining tile count after removing factory tiles = " + remainingTiles.Count);
            return remainingTiles;
        }
    }
}
