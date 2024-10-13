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

        private int _endGameScore;

        public int EndGameScore
        {
            get { return _endGameScore; }
            set { _endGameScore = value; }
        }

        public Player(string name)
        {
            this.name = name;
            score = GameConstants.STARTING_PLAYER_SCORE;
            playerBoard = new PlayerBoard();
            _endGameScore = 0;
        }

        public void ResetPlayerScore()
        {
            score = GameConstants.STARTING_PLAYER_SCORE; 
        }

        public void UpdatePlayerScore(int[] WallTileScore, int droppedTileScore)
        {
            int totalToAdd = 0;
            totalToAdd += WallTileScore.Sum() + droppedTileScore;

            if((score + totalToAdd) < 0)
            {
                score = 0;
            } else
            {
                score += totalToAdd;
            }
        }

        internal void CalculateEndGameScores()
        {
            // TODO - EndGameScore is only for testing purposes. Evaulate later if it should be kept or not.
            EndGameScore += playerBoard.CheckForFullRowsAndColumns() + playerBoard.CheckIfAllTilesOfTypeAreFilled(); ;
            score += EndGameScore;
        }
    }
}
