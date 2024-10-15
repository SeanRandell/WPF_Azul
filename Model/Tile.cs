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
        private int factoriesIndex;

        public int FactoriesIndex
        {
            get { return factoriesIndex; }
            set { factoriesIndex = value; }
        }

        private TileType _tileType;
        public TileType TileType { get { return _tileType; } }

        public Tile(TileType tileType)
        {
            _tileType = tileType;
            factoriesIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
        }
    }
}
