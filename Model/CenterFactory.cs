using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class CenterFactory : Factory
    {
        public void ResetCenterFactory()
        {

        }

        public bool ContainsStartingPlayerMarker()
        {
            foreach (var tile in factoryTiles)
            {
                if (tile.TileType == TileType.StartingPlayerMarker)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
