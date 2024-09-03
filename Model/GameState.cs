using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class GameState
    {
        private const int factoryAmount = 5;
        private int[] droppedTileCost = [-1, -1, -2, -2, -2, -3, -3];
        private TileType[,] tilePattern = {
            { TileType.Blue, TileType.Yellow, TileType.Red, TileType.Black, TileType.LightBlue},
            { TileType.LightBlue, TileType.Blue, TileType.Yellow, TileType.Red, TileType.Black},
            { TileType.Black, TileType.LightBlue, TileType.Blue, TileType.Yellow, TileType.Red},
            { TileType.Red, TileType.Black, TileType.LightBlue, TileType.Blue, TileType.Yellow},
            { TileType.Yellow, TileType.Red, TileType.Black, TileType.LightBlue, TileType.Blue}
        };

        private int droppedTileLength;

        private List<Player> players;
        private List<Factory> Factories;
        private int activePlayerTurnIndex;

        public GameState()
        {
            players = new List<Player>();
            Factories = new List<Factory>();
            activePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;
        }

        public TileType[,] GetWallPattern()
        {
            return tilePattern;
        }
    }
}
