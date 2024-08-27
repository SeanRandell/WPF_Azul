using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class Factory
    {
        private const int NORMAL_FACTORY_MAX_TILES = 4;
        protected List<Tile> factoryTiles;

        public Factory()
        {
            factoryTiles = new List<Tile>();
        }

        public void SetupFactory(TileCollections tileCollections)
        {
            for (int i = 0; i < NORMAL_FACTORY_MAX_TILES; i++)
            {
                AddFactoryTile(tileCollections.GetRandomTileFromBag());
            }
        }

        public void AddFactoryTile(Tile tile) 
        {
            factoryTiles.Add(tile);
        }

        public List<Tile> TakeAllTilesOfType(TileType currentTileType)
        {

            return new List<Tile>();
        }

    }
}
