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
        private int x;
        private int y;

        private TileType _tileType;
        public TileType TileType { get { return _tileType; }}

        private Color tileTypeColour;
        public Color TileTypeColour
        {
            get
            {
                return tileTypeColour;
            }
        }

        public Tile(TileType tileType, int x, int y)
        {
            _tileType = tileType;
            //ColorConverter colorConverter = new ColorConverter();
            tileTypeColour = (Color)ColorConverter.ConvertFromString(_tileType.ToString());
            this.x = x;
            this.y = y;
        }
    }
}
