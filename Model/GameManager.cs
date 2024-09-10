﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPF_Azul.Model
{
    public class GameManager
    {
        private GameState GameState;
        public GameManager()
        {
            GameState = new GameState();
        }

        public void UpdatePlayerProductionTiles(ObservableCollection<ObservableCollection<Tile>> playerProductionTiles, int playerIndex)
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    playerProductionTiles[i][j] = GameState.players[playerIndex].PlayerBoard.ProductionTiles[i][j];
                }
            }
        }

        public void UpdatePlayerWallTiles(ObservableCollection<ObservableCollection<Tile>> playerWallTiles, int playerIndex)
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    playerWallTiles[i][j] = GameState.players[playerIndex].PlayerBoard.WallTiles[i, j];
                }
            }
        }

        public void UpdatePlayerDroppedTiles(ObservableCollection<Tile> playerDroppedTiles, int playerIndex)
        {
            for (int i = 0; i < GameConstants.DROPPED_TILE_LENGTH; i++)
            {
                playerDroppedTiles[i] = GameState.players[playerIndex].PlayerBoard.DroppedTiles[i];
            }
        }

        // TODO - edit to be a list of list of tiles instead of a list of factories
        public void UpdateFactories(ObservableCollection<ObservableCollection<Tile>> factoryList)
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                for (int j = 0; j < GameState.Factories[i].FactoryTiles.Count; j++)
                {
                    factoryList[i][j] = GameState.Factories[i].FactoryTiles[j];
                }
            }
        }

        public ObservableCollection<Tile> UpdateCenterFactoryTiles()
        {
            ObservableCollection<Tile> returnList = new ObservableCollection<Tile>();
            for (int i = 0; i < GameState.CenterFactory.FactoryTiles.Count; i++)
            {
                returnList.Add(GameState.CenterFactory.FactoryTiles[i]);
            }
            return returnList;
        }

        public void FactoryTileClick()
        {

        }

        public void TestAddDroppedTile()
        {
            GameState.players[0].PlayerBoard.DroppedTiles[0] = new Tile(TileType.Yellow);
        }

        public List<int> GetValidProductionTiles(TileType selectedTileType)
        {
            List<int> resultList = GameState.players[GameState.activePlayerTurnIndex].PlayerBoard.GetValidProductionTilesIndexes(selectedTileType);
            // repeat for each production line
            foreach (var item in resultList)
            {
                
            }
            // first check if production line is empty 
            // then check if there is not already that tile type in the wall tile row

            // check if the production line is full

            // if there space but there is already tiles in this row
            // check if there is already the same type of tile in the production line
            return resultList;
        }
    }
}
