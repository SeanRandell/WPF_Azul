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
        public static int PLAYER_TWO_INDEX = 1;
        public static int STARTING_PLAYER_SCORE = 0;
        public static int DEFAULT_PLAYER_COUNT = 2;
        public static int DROPPED_TILE_LENGTH = 7;
        public static int MAIN_TILES_LENGTH = 5;
        public static int FACTORY_COUNT = 5;
        public static int ALL_FACTORY_COUNT = 6;
        public static int CENTER_FACTORY_INDEX = 5;
        public static int NORMAL_FACTORY_MAX_TILES = 4;
        public static int[] DROPPED_TILE_COSTS = [-1, -1, -2, -2, -2, -3, -3];
        public static int ALL_PRODUCTION_ROWS_COUNT = 6;
        public static int DROPPED_TILE_ROW_INDEX = 5;

        public static int COMPLETED_ROW_SCORE = 2;
        public static int COMPLETED_COLUMN_SCORE = 7;
        public static int COMPLETED_COLOUR_SCORE = 10;

        public static int FLOOR_LINE_MIN_LENGTH = 1;
        public static int FLOOR_LINE_MAX_LENGTH = 5;
        public static TileType[,] WALL_TILE_PATTERN = {
            { TileType.Blue, TileType.Yellow, TileType.Red, TileType.Black, TileType.LightBlue},
            { TileType.LightBlue, TileType.Blue, TileType.Yellow, TileType.Red, TileType.Black},
            { TileType.Black, TileType.LightBlue, TileType.Blue, TileType.Yellow, TileType.Red},
            { TileType.Red, TileType.Black, TileType.LightBlue, TileType.Blue, TileType.Yellow},
            { TileType.Yellow, TileType.Red, TileType.Black, TileType.LightBlue, TileType.Blue}
        };

        public static int TILE_NOT_IN_LIST_INDEX = -1;


        public static int GetWallTilePatternIndex(int rowIndex, TileType tileTypeToFind)
        {
            int defaultReturnInt = -1;
            TileType currentType;
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                currentType = WALL_TILE_PATTERN[rowIndex, i];
                if (currentType == tileTypeToFind)
                {
                    return i;
                }
            }
            return defaultReturnInt;
        }
    }
}
