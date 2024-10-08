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
        private GameState _gameState;
        public GameManager()
        {
            _gameState = new GameState();
        }

        public void UpdatePlayerProductionTiles(ObservableCollection<ObservableCollection<Tile>> playerProductionTiles, int playerIndex)
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    playerProductionTiles[i][j] = _gameState.players[playerIndex].PlayerBoard.ProductionTiles[i][j];
                }
            }
        }

        public void UpdatePlayerWallTiles(ObservableCollection<ObservableCollection<Tile>> playerWallTiles, int playerIndex)
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    playerWallTiles[i][j] = _gameState.players[playerIndex].PlayerBoard.WallTiles[i, j];
                }
            }
        }

        public void UpdatePlayerDroppedTiles(ObservableCollection<Tile> playerDroppedTiles, int playerIndex)
        {
            for (int i = 0; i < GameConstants.DROPPED_TILE_LENGTH; i++)
            {
                playerDroppedTiles[i] = _gameState.players[playerIndex].PlayerBoard.DroppedTiles[i];
            }
        }

        public void UpdateFactories(ObservableCollection<ObservableCollection<Tile>> factoryList)
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                for (int j = 0; j < _gameState.Factories[i].FactoryTiles.Count; j++)
                {
                    //if (factoryList[i][j] != null)
                    //{
                    //    factoryList[i][j] = GameState.Factories[i].FactoryTiles[j];
                    //}
                    //else
                    //{
                    //    factoryList[i][j] = null;
                    //}
                    factoryList[i] = UpdateFactory(i);
                }
            }
        }

        public void UpdateFactory(ObservableCollection<Tile> factoryTiles, int factoryIndex)
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {

                //if (factoryList[i][j] != null)
                //{
                //    factoryList[i][j] = GameState.Factories[i].FactoryTiles[j];
                //}
                //else
                //{
                //    factoryList[i][j] = null;
                //}
                factoryTiles[i] = _gameState.Factories[factoryIndex].FactoryTiles[i];

            }
        }

        public ObservableCollection<Tile> UpdateCenterFactoryTiles()
        {
            ObservableCollection<Tile> returnList = new ObservableCollection<Tile>();
            for (int i = 0; i < _gameState.CenterFactory.FactoryTiles.Count; i++)
            {
                returnList.Add(_gameState.CenterFactory.FactoryTiles[i]);
            }
            return returnList;
        }

        public List<int> GetValidProductionTiles(TileType selectedTileType)
        {
            List<int> resultList = _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.GetValidProductionTilesIndexes(selectedTileType);

            return resultList;
        }

        public int GetCurrentPlayerTurn()
        {
            return _gameState.activePlayerTurnIndex;
        }

        public void ProductionTileSelected(int productionTileIndex, TileType selectedTileType, int selectedFactoryIndex)
        {
            // get list of tiles being moved from factory and remove them
            List<Tile> selectedFactoryTiles;
            if (selectedFactoryIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                selectedFactoryTiles = _gameState.CenterFactory.TakeAllTilesOfType(selectedTileType);
                _gameState.CenterFactory.ProcessFactoryTilesSelectedForProduction(selectedTileType);
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

        public void UpdateProductionTiles(ObservableCollection<Tile> prodcutionTile, int productionTileIndex)
        {
            for (int i = 0; i < _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.ProductionTiles[productionTileIndex].Length; i++)
            {
                prodcutionTile[i] = _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.ProductionTiles[productionTileIndex][i];
            }
        }

        public ObservableCollection<Tile> UpdateFactory(int selectedFactoryIndex)
        {
            ObservableCollection<Tile> newFactoryTileList = new ObservableCollection<Tile>();
            for (int i = 0; i < _gameState.Factories[selectedFactoryIndex].FactoryTiles.Count; i++)
            {
                newFactoryTileList.Add(_gameState.Factories[selectedFactoryIndex].FactoryTiles[i]);
            }
            return newFactoryTileList;
        }

        public void UpdateDroppedTiles(ObservableCollection<Tile> droppedTiles)
        {
            for (int i = 0; i < _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.DroppedTiles.Length; i++)
            {
                droppedTiles[i] = _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.DroppedTiles[i];
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
            for (int i = 0; i < _gameState.players.Count; i++)
            {
                //first update walltile scores and process them and empty production tiles
                _gameState.players[i].PlayerBoard.CalculateProductionTileScores(_gameState.tileCollections);
                _gameState.players[i].PlayerBoard.CalculateDroppedTileScores(_gameState.tileCollections);

                //minus the content of dropped tiles from player score
                // empty dropped tiles

                _gameState.players[i].UpdatePlayerScore(_gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.WallTileScores, _gameState.players[i].PlayerBoard.DroppedTileScore);
            }
            // replenish factories
            _gameState.SetupFactoriesForRound();
        }

        internal List<int> GetPlayerWallScores()
        {
            // This involves checking if a production tile is full and then moving the tile to the player wall
            return _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.WallTileScores.ToList();
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

            //check if player 1 or player 2 is the winner for now. TODO - do this with iteration instead.
            // Set gamephase

        }

        internal bool IsGameOver()
        {
            if(_gameState._gamePhase == GamePhase.Player1Wins || _gameState._gamePhase == GamePhase.Player2Wins || _gameState._gamePhase == GamePhase.Draw)
            {
                return true;
            }
            return false;
        }

        internal int GetPlayerDroppedTileScore()
        {
            return _gameState.players[_gameState.activePlayerTurnIndex].PlayerBoard.DroppedTileScore;
        }

        internal int GetTotalPlayerScore()
        {
            return (int)_gameState.players[GameConstants.STARTING_PLAYER_INDEX].score;
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
