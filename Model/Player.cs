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

        private uint startingScore = 0;
        private PlayerBoard playerBoard;

        public PlayerBoard PlayerBoard
        {
            get { return playerBoard; }
            set { playerBoard = value; }
        }

        public Player(string name)
        {
            this.name = name;
            score = startingScore;
            playerBoard = new PlayerBoard();
        }

        public void ResetPlayerScore()
        {
            score = startingScore; 
        }
    }
}
