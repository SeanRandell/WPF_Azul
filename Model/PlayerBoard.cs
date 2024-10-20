using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Azul.Model
{
    public class PlayerBoard
    {
        public Tile[,] WallTiles
        {
            get;
            set;
        }

        public List<List<Tile>> ProductionTiles
        {
            get;
            set;
        }

        public List<Tile> DroppedTiles
        {
            get;
            set;
        }

        //TODO - these score calculations may not need to be saved. Find a better way of getting them to the viewmodel only when needed.

        public List<int> WallTileScores
        {
            get;
            set;
        }

        public int DroppedTileScore
        {
            get;
            set;
        }

        internal PlayerBoard()
        {
            WallTiles = new Tile[GameConstants.MAIN_TILES_LENGTH, GameConstants.MAIN_TILES_LENGTH];
            ProductionTiles = new List<List<Tile>>(GameConstants.MAIN_TILES_LENGTH);
            DroppedTiles = new List<Tile>(GameConstants.DROPPED_TILE_LENGTH);
            WallTileScores = new List<int>(GameConstants.MAIN_TILES_LENGTH);
            DroppedTileScore = 0;

            InitWallTiles();
            InitProductionTiles();
        }

        private void InitProductionTiles()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                List<Tile> newProdoctionTileRow = new List<Tile>();

                for (int j = 0; j < i + 1; j++)
                {
                    newProdoctionTileRow.Add(null);
                }
                ProductionTiles.Add(newProdoctionTileRow);
            }
        }

        private void InitWallTiles()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    WallTiles[i, j] = null;
                }
            }
        }

        internal List<int> GetValidProductionTilesIndexes(TileType selectedTileType)
        {
            List<int> resultList = new List<int>();

            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                // first check if there is not already that tile type in the wall tile row
                if (GetRow(WallTiles, i).Any(tile => tile != null && tile.TileType == selectedTileType))
                {
                    continue;
                }

                bool doesTheProductionLineHaveEmptySpaces = ProductionTiles[i].Contains(null);
                if (!doesTheProductionLineHaveEmptySpaces)
                {
                    continue;
                }

                bool sameTileTypeInProductionLine = ProductionTiles[i].Any(tile => tile != null && tile.TileType == selectedTileType);
                if (sameTileTypeInProductionLine && doesTheProductionLineHaveEmptySpaces)
                {
                    resultList.Add(i);
                    continue;
                }

                bool isProductionLineEmpty = ProductionTiles[i].All(tile => tile == null);
                if (isProductionLineEmpty)
                {
                    resultList.Add(i);
                }
            }
            return resultList;
        }

        internal Tile[] GetRow(Tile[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        internal void AddTilesToProductionTiles(int productionTileIndex, List<Tile> selectedFactoryTiles)
        {
            if (productionTileIndex != GameConstants.DROPPED_TILE_ROW_INDEX)
            {
                // loop through adding the selected tiles to production tiles until the production tiles are full and then when the selected tiles are empty.
                int i = ProductionTiles[productionTileIndex].FindIndex(tile => tile == null);
                int tilesAddedToProductionCount = 0;
                for (; i >= 0 && i < ProductionTiles[productionTileIndex].Count && tilesAddedToProductionCount < selectedFactoryTiles.Count; i++)
                {
                    if (ProductionTiles[productionTileIndex][i] == null)
                    {
                        ProductionTiles[productionTileIndex][i] = selectedFactoryTiles[tilesAddedToProductionCount];
                        tilesAddedToProductionCount++;
                    }
                }
                selectedFactoryTiles.RemoveRange(0, tilesAddedToProductionCount);
            }

            if (selectedFactoryTiles.Count > 0 || productionTileIndex == GameConstants.DROPPED_TILE_ROW_INDEX)
            {
                Trace.WriteLine("leftover: " + selectedFactoryTiles.Count);
                AddTilesToDroppedTiles(selectedFactoryTiles);
            }
            else
            {
                Trace.WriteLine("All Selected tiles used");
            }

            // may need to return this list depending on how we want to do changes to selected tiles list.
        }

        internal void AddTilesToDroppedTiles(List<Tile> selectedFactoryTiles)
        {
            // first find the first null index and start adding tiles there
            int i = DroppedTiles.Count;
            Trace.WriteLine("First index of null value in dropped tiles is " + i);

            int tilesAddedToDroppedTiles = 0;
            for (; i >= 0 && i < GameConstants.DROPPED_TILE_LENGTH && tilesAddedToDroppedTiles < selectedFactoryTiles.Count; i++)
            {
                DroppedTiles.Add(selectedFactoryTiles[tilesAddedToDroppedTiles]);
                tilesAddedToDroppedTiles++;
            }

            // once dropped tile is full or all tiles have been added. remove the added tiles from selected tiles
            selectedFactoryTiles.RemoveRange(0, tilesAddedToDroppedTiles);
            // check if selected tiles still contains items.
            Trace.WriteLine("leftover dropped tiles: " + selectedFactoryTiles.Count);
        }

        internal bool TryToAddTileToDroppedTiles(Tile tileToAdd)
        {
            // first find the first null index and start adding tiles there
            if (DroppedTiles.Count < GameConstants.DROPPED_TILE_LENGTH)
            {
                DroppedTiles.Add(tileToAdd);
                return true;
            }
            return false;
        }

        internal void CalculateProductionTileScores(TileCollections tileCollections)
        {
            List<int> returnList = new List<int>(GameConstants.MAIN_TILES_LENGTH);

            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                // Move tiles out of productionTiles
                if (!ProductionTiles[i].Contains(null))
                {
                    int wallTileSlotIndex = 0;
                    for (int j = 0; j < ProductionTiles[i].Count; j++)
                    {
                        if (j == 0)
                        {
                            wallTileSlotIndex = GameConstants.GetWallTilePatternIndex(i, ProductionTiles[i][j].TileType);
                            WallTiles[i, wallTileSlotIndex] = ProductionTiles[i][j];
                        }
                        else
                        {
                            tileCollections.tileBin.Add(ProductionTiles[i][j]);

                        }
                        ProductionTiles[i][j] = null;
                    }

                    // Check connected tiles in the row
                    int rowScore = 1;
                    rowScore += CountConnectedTiles(i, wallTileSlotIndex, 0, 1); // Check east
                    rowScore += CountConnectedTiles(i, wallTileSlotIndex, 0, -1); // Check west

                    // Check connected tiles in the column
                    int columnscore = 1;
                    columnscore += CountConnectedTiles(i, wallTileSlotIndex, 1, 0); // Check south
                    columnscore += CountConnectedTiles(i, wallTileSlotIndex, -1, 0); // Check north

                    //Subtract 1 because the starting tile is counted twice.
                    returnList.Add(rowScore + columnscore - 1);
                }
                else
                {
                    returnList.Add(0);
                }
            }

            WallTileScores = returnList;
        }

        private int CountConnectedTiles(int x, int y, int xDirection, int yDirection)
        {
            int count = 0;
            int newX = x + xDirection;
            int newY = y + yDirection;

            while (IsValidWallTile(newX, newY) && WallTiles[newX, newY] != null)
            {
                Trace.WriteLine("WallTile[" + x + ", " + y + "] is the start");
                Trace.WriteLine("WallTile[" + newX + "," + newY + "] = " + WallTiles[newX, newY].TileType);
                count++;
                newX += xDirection;
                newY += yDirection;
            }

            return count;
        }

        internal void CalculateDroppedTileScores(TileCollections tileCollections)
        {
            int totalScore = 0;
            for (int i = 0; i < DroppedTiles.Count; i++)
            {
                totalScore += GameConstants.DROPPED_TILE_COSTS[i];
                tileCollections.tileBin.Add(DroppedTiles[i]);

                DroppedTiles.RemoveAt(i);
            }
            DroppedTileScore = totalScore;
        }

        private bool IsValidWallTile(int x, int y)
        {
            return x >= 0 && x < WallTiles.GetLength(0) && y >= 0 && y < WallTiles.GetLength(1);
        }

        internal bool CheckIfPlayerEndedGame()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                if (IsFullRow(i))
                {
                    return true;
                }
            }
            return false;
        }

        internal int CheckForFullRowsAndColumns()
        {
            int score = 0;

            // Check rows
            for (int row = 0; row < WallTiles.GetLength(0); row++)
            {
                if (IsFullRow(row))
                {
                    score += GameConstants.COMPLETED_ROW_SCORE;
                }
            }

            // Check columns
            for (int column = 0; column < WallTiles.GetLength(1); column++)
            {
                if (IsFullColumn(column))
                {
                    score += GameConstants.COMPLETED_COLUMN_SCORE;
                }
            }

            return score;
        }

        private bool IsFullRow(int row)
        {
            for (int column = 0; column < WallTiles.GetLength(1); column++)
            {
                if (WallTiles[row, column] == null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsFullColumn(int column)
        {
            for (int row = 0; row < WallTiles.GetLength(0); row++)
            {
                if (WallTiles[row, column] == null)
                {
                    return false;
                }
            }
            return true;
        }

        internal int CheckIfAllTilesOfTypeAreFilled()
        {
            int returnScore = 0;
            List<List<TileType>> listOfTileTypes = new List<List<TileType>>();
            for (int i = 0; i < Enum.GetValues(typeof(TileType)).Length - 1; i++)
            {
                listOfTileTypes.Add(new List<TileType>());
            }

            int currentTileTypeIndex = 0;
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    if (WallTiles[i, j] != null)
                    {
                        currentTileTypeIndex = (int)WallTiles[i, j].TileType;
                        listOfTileTypes[currentTileTypeIndex].Add(WallTiles[i, j].TileType);
                    }
                }
            }

            for (int i = 0; i < listOfTileTypes.Count; i++)
            {
                if (listOfTileTypes[i].Count == GameConstants.MAIN_TILES_LENGTH)
                {
                    returnScore += GameConstants.COMPLETED_COLOUR_SCORE;
                }
            }

            return returnScore;
        }

        internal bool ContainsStartingTileMarker()
        {
            return DroppedTiles.Any(tile => tile.TileType == TileType.StartingPlayerMarker);
        }

        internal void ClearWallTilesForGameEnd(TileCollections tileCollections)
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    if (WallTiles[i, j] != null)
                    {
                        tileCollections.AddTileToTileBin(WallTiles[i, j]);
                        WallTiles[i, j] = null;
                    }
                }
            }
        }

        internal void ClearProductionTilesForGameEnd(TileCollections tileCollections)
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < ProductionTiles[i].Count; j++)
                {
                    if (ProductionTiles[i][j] != null)
                    {
                        tileCollections.AddTileToTileBin(ProductionTiles[i][j]);
                        ProductionTiles[i][j] = null;
                    }
                }
            }
        }

        internal void ClearDroppedTilesForGameEnd(TileCollections tileCollections)
        {
            tileCollections.AddTilesToTileBin(DroppedTiles);
            DroppedTiles.Clear();
        }
    }
}
