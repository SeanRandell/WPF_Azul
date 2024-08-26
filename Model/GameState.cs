using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    internal class GameState
    {
        const int factoryAmount = 5;
        private int[] droppedTileCost = [-1, -1, -2, -2, -2, -3, -3];
        private int droppedTileLength;

        private List<Player> players;
        private List<Factory> Factories;
        private int activePlayerTurnIndex;

        private int STARTING_PLAYER_INDEX = 0;

        public GameState()
        {
            players = new List<Player>();
            Factories = new List<Factory>();
            activePlayerTurnIndex = STARTING_PLAYER_INDEX;
        }


    }
}
