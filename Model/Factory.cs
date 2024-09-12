using System;
using System.Collections.Generic;
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

        public List<Tile> TakeAllTilesOfType(TileType currentTileType)
        {
            return factoryTiles.Where(t => t.TileType == currentTileType).ToList();
        }

        public void RemoveAllTilesOfType(TileType currentTileType)
        {
            factoryTiles.RemoveAll(t => t.TileType == currentTileType);
        }

    }
}
