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

        public void UpdateFactories(ObservableCollection<ObservableCollection<Tile>> factoryList)
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                for (int j = 0; j < GameState.Factories[i].FactoryTiles.Count; j++)
                {
                    //if (factoryList[i][j] != null)
                    //{
                    //    factoryList[i][j] = GameState.Factories[i].FactoryTiles[j];
                    //}
                    //else
                    //{
                    //    factoryList[i][j] = null;
                    //}
                    factoryList[i][j] = GameState.Factories[i].FactoryTiles[j];
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
                factoryTiles[i] = GameState.Factories[factoryIndex].FactoryTiles[i];

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
            //foreach (int validIndex in resultList)
            //{
            //    vali
            //}

            return resultList;
        }

        public int GetCurrentPlayerTurn()
        {
            return GameState.activePlayerTurnIndex;
        }

        public void ProductionTileSelected(int productionTileIndex, TileType selectedTileType, int selectedFactoryIndex)
        {
            // get list of tiles being moved from factory and remove them
            List<Tile> selectedFactoryTiles = GameState.Factories[selectedFactoryIndex].TakeAllTilesOfType(selectedTileType);
            GameState.Factories[selectedFactoryIndex].RemoveAllTilesOfType(selectedTileType);

            //add as many tiles as you can from the list to the production tile
            GameState.players[GameState.activePlayerTurnIndex].PlayerBoard.AddTilesToProductionTiles(productionTileIndex, selectedFactoryTiles);

            // if factory tile count > 0 then add them to next null dropped tile index
            if (selectedFactoryTiles.Count > 0)
            {
                Trace.WriteLine(selectedFactoryTiles.Count + " tiles left");
            }
            // TODO - manage edge case of a full dropped tiles list. Tiles that do not fit in the list get added to tile bin. Playerboard does not manage this. TileCollections does.
        }

        public void UpdateProductionTile(ObservableCollection<Tile> prodcutionTile, int productionTileIndex)
        {
            for (int i = 0; i < GameState.players[GameConstants.STARTING_PLAYER_INDEX].PlayerBoard.ProductionTiles[productionTileIndex].Length; i++)
            {
                prodcutionTile[i] = GameState.players[GameConstants.STARTING_PLAYER_INDEX].PlayerBoard.ProductionTiles[productionTileIndex][i];
            }
        }

        public ObservableCollection<Tile> UpdateFactory(int selectedFactoryIndex)
        {
            ObservableCollection<Tile> newFactoryTileList = new ObservableCollection<Tile>();
            for (int i = 0; i < GameState.Factories[selectedFactoryIndex].FactoryTiles.Count; i++)
            {
                newFactoryTileList.Add(GameState.Factories[selectedFactoryIndex].FactoryTiles[i]);
            }
            return newFactoryTileList;
        }
    }
}
