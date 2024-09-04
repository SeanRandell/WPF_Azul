using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class GameState
    {
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

        private void InitPlayers()
        {
            Player player1 = new Player("Player2");
            Player player2 = new Player("Player1");
            players.Add(player1);
            players.Add(player2);
        }
    }
}
