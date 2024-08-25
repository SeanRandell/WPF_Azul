using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF_Azul.Model
{
    public class Tile
    {
        private TileType _tileType;
        public TileType TileType { get { return _tileType; }}

        public Tile(TileType tileType)
        {
            _tileType = tileType;

        }
    }
}
