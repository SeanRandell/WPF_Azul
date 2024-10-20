using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class Player
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public PlayerBoard PlayerBoard
        {
            get;
            set;
        }

        public int EndGameScore
        {
            get;
            set;
        }

        public Player(string name)
        {
            this.Name = name;
            Score = GameConstants.STARTING_PLAYER_SCORE;
            PlayerBoard = new PlayerBoard();
            EndGameScore = 0;
        }

        public void ResetPlayerScore()
        {
            Score = GameConstants.STARTING_PLAYER_SCORE; 
        }

        public void UpdatePlayerScore(List<int> WallTileScore, int droppedTileScore)
        {
            int totalToAdd = 0;
            totalToAdd += WallTileScore.Sum() + droppedTileScore;

            if((Score + totalToAdd) < 0)
            {
                Score = 0;
            } else
            {
                Score += totalToAdd;
            }
        }

        internal void CalculateEndGameScores()
        {
            // TODO - EndGameScore is only for testing purposes. Evaulate later if it should be kept or not.
            EndGameScore += PlayerBoard.CheckForFullRowsAndColumns() + PlayerBoard.CheckIfAllTilesOfTypeAreFilled();
            Score += EndGameScore;
        }
    }
}
