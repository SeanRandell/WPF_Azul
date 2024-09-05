using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class Player
    {
        public string name { get; set; }
        private uint score { get; set; }

        private PlayerBoard playerBoard;
        public PlayerBoard PlayerBoard
        {
            get { return playerBoard; }
            set { playerBoard = value; }
        }

        public Player(string name)
        {
            this.name = name;
            score = GameConstants.STARTING_PLAYER_SCORE;
            playerBoard = new PlayerBoard();
        }

        public void ResetPlayerScore()
        {
            score = GameConstants.STARTING_PLAYER_SCORE; 
        }
    }
}
