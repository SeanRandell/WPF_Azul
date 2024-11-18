using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPF_Azul.Model
{
    public class GameManager
    {
        internal GameState GameState { get; set; }

        internal GameManager()
        {
            GameState = new GameState();
        }

        internal Player GetPlayer(int index)
        {
            return GameState.Players[index];
        }

        internal List<int> GetValidProductionTiles(TileType selectedTileType)
        {
            List<int> resultList = GameState.Players[GameState.ActivePlayerTurnIndex].PlayerBoard.GetValidProductionTilesIndexes(selectedTileType);

            return resultList;
        }

        internal void ProductionTileSelected(int productionTileIndex, int selectedFactoryIndex)
        {
            // get list of tiles being moved from factory and remove them
            if (selectedFactoryIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                GameState.CenterFactory.ProcessFactoryTilesSelectedForProduction();
                GameState.CenterFactory.CheckAndProcessIfFirstSelected(GameState.Players[GameState.ActivePlayerTurnIndex].PlayerBoard, GameState.TileCollections);
            }
            else
            {
                GameState.Factories[selectedFactoryIndex].ProcessFactoryTilesSelectedForProduction();

                GameState.CenterFactory.AddTiles(GameState.Factories[selectedFactoryIndex].RemoveRemainingTiles());
            }

            //add as many tiles as you can from the list to the production tile and if factory tile count > 0 then add them to next null dropped tile index
            GameState.Players[GameState.ActivePlayerTurnIndex].PlayerBoard.AddTilesToProductionTiles(productionTileIndex, GameState.SelectedFactoryTiles);

            // manage edge case of a full dropped tiles list. Tiles that do not fit in the list get added to tile bin. Playerboard does not manage this. TileCollections does.
            if (GameState.SelectedFactoryTiles.Count > 0)
            {
                Trace.WriteLine(GameState.SelectedFactoryTiles.Count + " tiles to be added to tile bin");
                Trace.WriteLine("TileBin Count = " + GameState.TileCollections.tileBin.Count);
                GameState.TileCollections.AddTilesToTileBin(GameState.SelectedFactoryTiles);
                GameState.SelectedFactoryTiles.Clear();
                Trace.WriteLine("TileBin Count = " + GameState.TileCollections.tileBin.Count);
            }
        }

        internal void ClearSelectedFactoryTiles()
        {
            GameState.SelectedFactoryTiles.Clear();
        }

        internal int GetDebugTileBagCount()
        {
            return GameState.TileCollections.tileBag.Count;
        }

        internal int GetDebugTileBinCount()
        {
            return GameState.TileCollections.tileBin.Count;
        }

        internal void StartGame()
        {
            GameState.GamePhase = GamePhase.PlayingRound;
        }

        internal void CheckForRoundEndAndProcess()
        {
            //loop through all factories and center factory to make sure they are empty.

            bool doFactoriesStillHaveTiles = false;
            foreach (Factory factory in GameState.Factories)
            {
                if (factory.FactoryTiles.Any())
                {
                    doFactoriesStillHaveTiles = true;
                }
            }

            if (GameState.CenterFactory.FactoryTiles.Any())
            {
                doFactoriesStillHaveTiles = true;
            }

            // if yes, Change GamePhase to round over.
            if (!doFactoriesStillHaveTiles)
            {
                GameState.GamePhase = GamePhase.EndOfRound;
                ProcessRoundEnd();
            }
        }

        private void ProcessRoundEnd()
        {
            int indexOfplayerWithStartingMarker = 0;
            for (int i = 0; i < GameState.Players.Count; i++)
            {
                if (GameState.Players[i].PlayerBoard.ContainsStartingTileMarker())
                {
                    indexOfplayerWithStartingMarker = i;
                }
                //first update walltile scores and process them and empty production tiles
                GameState.Players[i].PlayerBoard.CalculateProductionTileScores(GameState.TileCollections);
                GameState.Players[i].PlayerBoard.CalculateDroppedTileScores(GameState.TileCollections);

                //minus the content of dropped tiles from player score
                // empty dropped tiles

                GameState.Players[i].UpdatePlayerScore(GameState.Players[i].PlayerBoard.WallTileScores, GameState.Players[i].PlayerBoard.DroppedTileScore);
            }
            GameState.ActivePlayerTurnIndex = indexOfplayerWithStartingMarker;

            CheckForGameOverAndProcess();

            if (!IsGameOver())
            {
                // replenish factories
                GameState.SetupFactoriesForRound();
            }
        }

        internal bool IsRoundOver()
        {
            if (GameState.GamePhase == GamePhase.EndOfRound)
            {
                return true;
            }
            return false;
        }

        internal void CheckForGameOverAndProcess()
        {
            for (int i = 0; i < GameState.Players.Count; i++)
            {
                if (GameState.Players[i].PlayerBoard.CheckIfPlayerEndedGame())
                {
                    StartGameEndProcessing();
                    break;
                }
            }
        }

        private void StartGameEndProcessing()
        {
            //tell each playerboard to calculate rows, columns and if each tile is present 5 times in the wall
            for (int i = 0; i < GameState.Players.Count; i++)
            {
                GameState.Players[i].CalculateEndGameScores();
            }

            // check if draw
            int amountOfSameScores = GameState.Players.GroupBy(s => s.Score).ToList().Count;
            if (amountOfSameScores == 1)
            {
                GameState.GamePhase = GamePhase.Draw;
            }
            else
            {
                //check if player 1 or player 2 is the winner for now. TODO - do this with iteration instead.
                int indexOfWinningPlayer = 0;
                int currentHighestScore = 0;
                for (int i = 0; i < GameState.Players.Count; i++)
                {
                    if (GameState.Players[i].Score > currentHighestScore)
                    {
                        indexOfWinningPlayer = i;
                        currentHighestScore = GameState.Players[i].Score;
                    }
                }

                if (indexOfWinningPlayer == GameConstants.STARTING_PLAYER_INDEX)
                {
                    GameState.GamePhase = GamePhase.Player1Wins;
                }
                else
                {
                    GameState.GamePhase = GamePhase.Player2Wins;
                }
            }
        }

        internal bool IsGameOver()
        {
            if (GameState.GamePhase == GamePhase.Player1Wins || GameState.GamePhase == GamePhase.Player2Wins || GameState.GamePhase == GamePhase.Draw)
            {
                return true;
            }
            return false;
        }

        internal void StartNewRound()
        {
            GameState.GamePhase = GamePhase.PlayingRound;
        }

        internal int GetCurrentPlayerIndex()
        {
            return GameState.ActivePlayerTurnIndex;
        }

        internal void ChangePlayerTurn()
        {
            GameState.ActivePlayerTurnIndex++;
            if (GameState.ActivePlayerTurnIndex == GameState.Players.Count)
            {
                GameState.ActivePlayerTurnIndex = 0;
            }
        }

        internal void RestartGame()
        {
            GameState.ResetGameFromGameEnd();
        }

        internal void UpdateSelectedFactoryTiles(TileType selectedTileType, int factoriesIndex)
        {
            if (factoriesIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                GameState.SelectedFactoryTiles = GameState.CenterFactory.TakeAllTilesOfType(selectedTileType);
            }
            else
            {
                GameState.SelectedFactoryTiles = GameState.Factories[factoriesIndex].TakeAllTilesOfType(selectedTileType);
            }
        }

        internal void PlaceSelectedFactoryTilesBack(int selectedFactoryIndex)
        {
            if (selectedFactoryIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                for (int i = 0; i < GameState.CenterFactory.FactoryTiles.Count; i++)
                {
                    if (GameState.CenterFactory.FactoryTiles[i] == null)
                    {
                        GameState.CenterFactory.FactoryTiles[i] = GameState.SelectedFactoryTiles.First();
                        GameState.SelectedFactoryTiles.Remove(GameState.SelectedFactoryTiles.First());
                    }
                }
            }
            else
            {
                for (int i = 0; i < GameState.Factories[selectedFactoryIndex].FactoryTiles.Count; i++)
                {
                    if (GameState.Factories[selectedFactoryIndex].FactoryTiles[i] == null)
                    {
                        GameState.Factories[selectedFactoryIndex].FactoryTiles[i] = GameState.SelectedFactoryTiles.First();
                        GameState.SelectedFactoryTiles.Remove(GameState.SelectedFactoryTiles.First());
                    }
                }
            }
        }

        internal void SetPlayerNames(List<string> playerNames)
        {
            for (int i = 0; i < GameState.Players.Count; i++)
            {
                GameState.Players[i].Name = playerNames[i];
            }
        }
    }
}
