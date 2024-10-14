using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF_Azul.Model;

namespace WPF_Azul.View.Converters
{
    class TileTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tileFilePathStart = "../../ViewModel/Assets/tile";
            string TileColour = "";
            string imageFileType = ".png";
            string returnString;

            if ((Tile)value == null)
            {
                returnString = "";
            }
            else
            {
                Tile currentTile = (Tile)value;
                TileType currentTileType = currentTile.TileType;

                switch (currentTileType)
                {
                    case TileType.Blue:
                        TileColour = "Blue";
                        break;
                    case TileType.Red:
                        TileColour = "Red";
                        break;
                    case TileType.Green:
                        TileColour = "Green";
                        break;
                    case TileType.Black:
                        TileColour = "Black";
                        break;
                    case TileType.Yellow:
                        TileColour = "Yellow";
                        break;
                    case TileType.StartingPlayerMarker:
                        TileColour = "StartingPlayerMarker";
                        break;
                    default:
                        TileColour = "";
                        break;
                }
            }
            returnString = tileFilePathStart + TileColour + imageFileType;
            return returnString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
