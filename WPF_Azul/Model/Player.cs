using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    internal class Player
    {
        internal string Name { get; set; }

        internal int Score { get; set; }

        internal PlayerBoard PlayerBoard
        {
            get;
            set;
        }

        internal int EndGameScore
        {
            get;
            set;
        }

        internal int PlayerIndex { get; set; }

        internal Player(string name)
        {
            this.Name = name;
            Score = GameConstants.STARTING_PLAYER_SCORE;
            PlayerBoard = new PlayerBoard();
            EndGameScore = 0;
        }

        internal void ResetPlayerScore()
        {
            Score = GameConstants.STARTING_PLAYER_SCORE; 
        }

        internal void UpdatePlayerScore(List<int> WallTileScore, int droppedTileScore)
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
