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
        internal GameState _gameState;
        public GameManager()
        {
            _gameState = new GameState();
        }

        public Player GetPlayer(int index)
        {
            return _gameState.players[index];
        }

        public List<int> GetValidProductionTiles(TileType selectedTileType)
        {
            List<int> resultList = _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.GetValidProductionTilesIndexes(selectedTileType);

            return resultList;
        }

        public void ProductionTileSelected(int productionTileIndex, TileType selectedTileType, int selectedFactoryIndex)
        {
            // get list of tiles being moved from factory and remove them
            List<Tile> selectedFactoryTiles;
            if (selectedFactoryIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                selectedFactoryTiles = _gameState.CenterFactory.TakeAllTilesOfType(selectedTileType);
                _gameState.CenterFactory.ProcessFactoryTilesSelectedForProduction(selectedTileType);
                _gameState.CenterFactory.CheckAndProcessIfFirstSelected(_gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard, _gameState.tileCollections);
            }
            else
            {
                selectedFactoryTiles = _gameState.Factories[selectedFactoryIndex].TakeAllTilesOfType(selectedTileType);
                _gameState.Factories[selectedFactoryIndex].ProcessFactoryTilesSelectedForProduction(selectedTileType);

                _gameState.CenterFactory.AddTiles(_gameState.Factories[selectedFactoryIndex].RemoveRemainingTiles());
            }

            //add as many tiles as you can from the list to the production tile
            _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.AddTilesToProductionTiles(productionTileIndex, selectedFactoryTiles);

            // if factory tile count > 0 then add them to next null dropped tile index
            //if (selectedFactoryTiles.Count > 0)
            //{
            //    GameState.players[GameConstants.STARTING_PLAYER_INDEX].PlayerBoard.AddTilesToDroppedTiles(selectedFactoryTiles);
            //    Trace.WriteLine(selectedFactoryTiles.Count + " tiles left");
            //}
            // TODO - manage edge case of a full dropped tiles list. Tiles that do not fit in the list get added to tile bin. Playerboard does not manage this. TileCollections does.
            if (selectedFactoryTiles.Count > 0)
            {
                Trace.WriteLine(selectedFactoryTiles.Count + " tiles to be added to tile bin");
                Trace.WriteLine("TileBin Count = " + _gameState.tileCollections.tileBin.Count);
                _gameState.tileCollections.AddTilesToTileBin(selectedFactoryTiles);
                Trace.WriteLine("TileBin Count = " + _gameState.tileCollections.tileBin.Count);
            }
        }

        public int GetDebugTileBagCount()
        {
            return _gameState.tileCollections.tileBag.Count;
        }

        public int GetDebugTileBinCount()
        {
            return _gameState.tileCollections.tileBin.Count;
        }

        internal void StartGame()
        {
            _gameState._gamePhase = GamePhase.PlayingRound;
        }

        internal void CheckForRoundEndAndProcess()
        {
            //loop through all factories and center factory to make sure they are empty.

            bool doFactoriesStillHaveTiles = false;
            foreach (Factory factory in _gameState.Factories)
            {
                if (factory.FactoryTiles.Any())
                {
                    doFactoriesStillHaveTiles = true;
                }
            }

            if (_gameState.CenterFactory.FactoryTiles.Any())
            {
                doFactoriesStillHaveTiles = true;
            }

            // if yes, Change GamePhase to round over.
            if (!doFactoriesStillHaveTiles)
            {
                _gameState._gamePhase = GamePhase.EndOfRound;
                ProcessRoundEnd();
            }
        }

        private void ProcessRoundEnd()
        {
            int indexOfplayerWithStartingMarker = 0;
            for (int i = 0; i < _gameState.players.Count; i++)
            {
                if (_gameState.players[i].PlayerBoard.ContainsStartingTileMarker())
                {
                    indexOfplayerWithStartingMarker = i;
                }
                //first update walltile scores and process them and empty production tiles
                _gameState.players[i].PlayerBoard.CalculateProductionTileScores(_gameState.tileCollections);
                _gameState.players[i].PlayerBoard.CalculateDroppedTileScores(_gameState.tileCollections);

                //minus the content of dropped tiles from player score
                // empty dropped tiles

                _gameState.players[i].UpdatePlayerScore(_gameState.players[i].PlayerBoard.WallTileScores, _gameState.players[i].PlayerBoard.DroppedTileScore);
            }
            _gameState.activePlayerTurnIndex = indexOfplayerWithStartingMarker;

            CheckForGameOverAndProcess();

            if (!IsGameOver())
            {
                // replenish factories
                _gameState.SetupFactoriesForRound();
            }
        }

        internal bool IsRoundOver()
        {
            if(_gameState._gamePhase == GamePhase.EndOfRound)
            {
                return true;
            }
            return false;
        }

        internal void CheckForGameOverAndProcess()
        {
            for (int i = 0; i < _gameState.players.Count; i++)
            {
                if (_gameState.players[i].PlayerBoard.CheckIfPlayerEndedGame())
                {
                    StartGameEndProcessing();
                    break;
                }
            }
        }

        private void StartGameEndProcessing()
        {
            //tell each playerboard to calculate rows, columns and if each tile is present 5 times in the wall
            for (int i = 0; i < _gameState.players.Count; i++)
            {
                _gameState.players[i].CalculateEndGameScores();
            }

            // check if draw
            int amountOfSameScores = _gameState.players.GroupBy(s => s.score).ToList().Count;
            if (amountOfSameScores == 1)
            {
                _gameState._gamePhase = GamePhase.Draw;
            }
            else
            {
                //check if player 1 or player 2 is the winner for now. TODO - do this with iteration instead.
                int indexOfWinningPlayer = 0;
                int currentHighestScore = 0;
                for (int i = 0; i < _gameState.players.Count; i++)
                {
                    if (_gameState.players[i].score > currentHighestScore)
                    {
                        indexOfWinningPlayer = i;
                        currentHighestScore = _gameState.players[i].score;
                    }
                }

                if (indexOfWinningPlayer == GameConstants.STARTING_PLAYER_INDEX)
                {
                    _gameState._gamePhase = GamePhase.Player1Wins;
                }
                else
                {
                    _gameState._gamePhase = GamePhase.Player2Wins;
                }
            }
        }

        internal bool IsGameOver()
        {
            if(_gameState._gamePhase == GamePhase.Player1Wins || _gameState._gamePhase == GamePhase.Player2Wins || _gameState._gamePhase == GamePhase.Draw)
            {
                return true;
            }
            return false;
        }

        internal void StartNewRound()
        {
            _gameState._gamePhase = GamePhase.PlayingRound;
        }

        internal int GetCurrentPlayerIndex()
        {
            return _gameState.activePlayerTurnIndex;
        }

        internal void ChangePlayerTurn()
        {
            _gameState.activePlayerTurnIndex++;
            if(_gameState.activePlayerTurnIndex == _gameState.players.Count)
            {
                _gameState.activePlayerTurnIndex = 0;
            }
        }
    }
}
