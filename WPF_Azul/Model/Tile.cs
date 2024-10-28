using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF_Azul.Model
{
    public class Tile
    {
        public int FactoriesIndex
        {
            get;
            set;
        }

        public TileType TileType { get; init; }

        public Tile(TileType tileType)
        {
            TileType = tileType;
            FactoriesIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
        }
    }
}
