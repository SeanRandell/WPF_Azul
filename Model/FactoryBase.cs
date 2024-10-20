using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    internal class FactoryBase
    {
        internal int FactoryIndex
        {
            get;
            set;
        }

        internal List<Tile> FactoryTiles { set;  get; }

        internal FactoryBase(int factoryIndex)
        {
            FactoryIndex = factoryIndex;
            FactoryTiles = new List<Tile>();
        }

        public int AddFactoryTile(Tile tile)
        {
            int preAdditionIndex = FactoryTiles.Count;
            FactoryTiles.Add(tile);
            return preAdditionIndex;
        }

        internal void AddTiles(List<Tile> tiles)
        {
            FactoryTiles.AddRange(tiles);
        }

        public List<Tile> TakeAllTilesOfType(TileType currentTileType)
        {
            List<Tile> tilesTakenFromFactory = new List<Tile> ();

            for (int i = 0; i < FactoryTiles.Count; i++)
            {
                if (FactoryTiles[i].TileType == currentTileType)
                {
                    tilesTakenFromFactory.Add(FactoryTiles[i]);
                    FactoryTiles[i] = null;
                }
            }

            return tilesTakenFromFactory;
        }

        public void ProcessFactoryTilesSelectedForProduction()
        {
            FactoryTiles.RemoveAll(tile => tile == null);
        }
    }
}
