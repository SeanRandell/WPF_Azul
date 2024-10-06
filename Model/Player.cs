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
        public int score { get; set; }

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

        public void UpdatePlayerScore(int[] WallTileScore, int droppedTileScore)
        {
            int totalToAdd = 0;
            totalToAdd += WallTileScore.Sum() + droppedTileScore;

            if((score += totalToAdd) < 0)
            {
                score = 0;
            } else
            {
                score += totalToAdd;
            }
        }
    }
}
