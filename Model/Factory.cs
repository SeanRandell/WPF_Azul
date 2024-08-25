using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class Factory
    {
        public List<Tile> factoryTiles;

        public Factory()
        {
            factoryTiles = new List<Tile>();
        }

        public void AddFactoryTile(Tile tile) {
        }

        public List<Tile> TakeAllTilesOfType(TileType tileType)
        {
            return new List<Tile>();
        }



    }
}
