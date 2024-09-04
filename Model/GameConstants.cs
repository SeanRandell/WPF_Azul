using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public static class GameConstants
    {
        public static int STARTING_PLAYER_INDEX = 0;
        public static int DEFAULT_PLAYER_COUNT = 2;
        public static int DROPPED_TILE_LENGTH = 7;
        public static int MAIN_TILES_LENGTH = 5;
        public static int FACTORY_COUNT = 5;
        public static int[] DROPPED_TILE_COSTS = [-1, -1, -2, -2, -2, -3, -3];
        public static int FLOOR_LINE_MIN_LENGTH = 1;
        public static int FLOOR_LINE_MAX_LENGTH = 5;
        public static TileType[,] WALL_TILE_PATTERN = {
            { TileType.Blue, TileType.Yellow, TileType.Red, TileType.Black, TileType.LightBlue},
            { TileType.LightBlue, TileType.Blue, TileType.Yellow, TileType.Red, TileType.Black},
            { TileType.Black, TileType.LightBlue, TileType.Blue, TileType.Yellow, TileType.Red},
            { TileType.Red, TileType.Black, TileType.LightBlue, TileType.Blue, TileType.Yellow},
            { TileType.Yellow, TileType.Red, TileType.Black, TileType.LightBlue, TileType.Blue}
        };
    }
}
