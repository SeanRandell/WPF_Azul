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
        private int wallTileX;
        public int WalltileX
        {
            get { return wallTileX; }
            set { wallTileX = value; }
        }

        private int wallTileY;
        public int WalltileY
        {
            get { return wallTileY; }
            set { wallTileY = value; }
        }

        private int productionRow;
        public int ProductionRow
        {
            get { return productionRow; }
            set { productionRow = value; }
        }

        private int productionRowIndex;

        public int ProductionRowIndex
        {
            get { return productionRowIndex; }
            set { productionRowIndex = value; }
        }

        private int factoriesIndex;

        public int FactoriesIndex
        {
            get { return factoriesIndex; }
            set { factoriesIndex = value; }
        }

        private TileType _tileType;
        public TileType TileType { get { return _tileType; } }

        private Color tileTypeColour;
        public Color TileTypeColour
        {
            get
            {
                return tileTypeColour;
            }
        }

        public Tile(TileType tileType)
        {
            _tileType = tileType;
            //ColorConverter colorConverter = new ColorConverter();
            if (_tileType == TileType.StartingPlayerMarker)
            {
                tileTypeColour = (Color)ColorConverter.ConvertFromString("Pink");
            }
            else
            {
                tileTypeColour = (Color)ColorConverter.ConvertFromString(_tileType.ToString());
            }

            wallTileX = GameConstants.TILE_NOT_IN_LIST_INDEX;
            wallTileX = GameConstants.TILE_NOT_IN_LIST_INDEX;
            productionRow = GameConstants.TILE_NOT_IN_LIST_INDEX;
            productionRowIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            factoriesIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
        }

        public Tile DeepClone(int wallTileX = -1, int wallTileY = -1)
        {
            Tile clonedTile = new Tile(TileType);
            clonedTile.wallTileX = wallTileX;
            clonedTile.wallTileY = wallTileY;
            return clonedTile;
        }
    }
}

