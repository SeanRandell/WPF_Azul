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
        private Tile[,] wallTiles;
        public Tile[,] WallTiles
        {
            get { return wallTiles; }
            set { wallTiles = value; }
        }

        private Tile[][] productionTiles;
        public Tile[][] ProductionTiles
        {
            get { return productionTiles; }
            set { productionTiles = value; }
        }

        private Tile[] droppedTiles;
        public Tile[] DroppedTiles
        {
            get { return droppedTiles; }
            set { droppedTiles = value; }
        }

        //TODO - these score calculations may not need to be saved. Find a better way of getting them to the viewmodel only when needed.
        private int[] _wallTileScores;

        public int[] WallTileScores
        {
            get { return _wallTileScores; }
            set { _wallTileScores = value; }
        }

        private int _droppedTileScore;

        public int DroppedTileScore
        {
            get { return _droppedTileScore; }
            set { _droppedTileScore = value; }
        }

        internal PlayerBoard()
        {
            wallTiles = new Tile[GameConstants.MAIN_TILES_LENGTH, GameConstants.MAIN_TILES_LENGTH];
            productionTiles = new Tile[GameConstants.MAIN_TILES_LENGTH][];
            droppedTiles = new Tile[GameConstants.DROPPED_TILE_LENGTH];
            _wallTileScores = new int[GameConstants.MAIN_TILES_LENGTH];
            _droppedTileScore = 0;

            InitWallTiles();
            InitProductionTiles();
        }

        private void InitProductionTiles()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                productionTiles[i] = new Tile[i + 1];
            }
        }

        private void InitWallTiles()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    wallTiles[i, j] = null;
                }
            }
        }

        // TODO - Find a way to optimise 
        internal List<int> GetValidProductionTilesIndexes(TileType selectedTileType)
        {
            List<int> resultList = new List<int>();

            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                // first check if there is not already that tile type in the wall tile row
                bool tileTypeExistsInWall = false;
                foreach (Tile tile in GetRow(wallTiles, i))
                {
                    if (tile != null)
                    {
                        if (tile.TileType == selectedTileType)
                        {
                            tileTypeExistsInWall = true;
                        }
                    }
                }
                if (tileTypeExistsInWall)
                {
                    continue;
                }

                // check if the production line is full
                if (Array.IndexOf(productionTiles[i], null) < 0)
                {
                    continue;
                }

                // if there space but there is already tiles in this row
                // check if there is already the same type of tile in the production line
                bool doesProductionLineMatchSelectedType = false;
                for (int j = 0; j < productionTiles[i].Length; j++)
                {
                    if (productionTiles[i][j] != null)
                    {
                        if (productionTiles[i][j].TileType == selectedTileType)
                        {
                            resultList.Add(i);
                            doesProductionLineMatchSelectedType = true;
                            break;
                        }
                    }
                }
                // if there is not matching tiles then check if production line is empty
                if (!doesProductionLineMatchSelectedType)
                {
                    bool isProductionLineEmpty = true;
                    for (int j = 0; j < productionTiles[i].Length; j++)
                    {
                        if (productionTiles[i][j] != null)
                        {
                            isProductionLineEmpty = false;
                        }
                    }
                    if (isProductionLineEmpty)
                    {
                        resultList.Add(i);
                        continue;
                    }
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
                int i = Array.IndexOf(productionTiles[productionTileIndex], null);
                int tilesAddedToProductionCount = 0;
                for (; i >= 0 && i < productionTiles[productionTileIndex].Length && tilesAddedToProductionCount < selectedFactoryTiles.Count; i++)
                {
                    if (productionTiles[productionTileIndex][i] == null)
                    {
                        productionTiles[productionTileIndex][i] = selectedFactoryTiles[tilesAddedToProductionCount];
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
            int i = Array.FindIndex(droppedTiles, t => t == null);
            Trace.WriteLine("First index of null value in dropped tiles is " + i);

            int tilesAddedToDroppedTiles = 0;
            for (; i >= 0 && i < droppedTiles.Length && tilesAddedToDroppedTiles < selectedFactoryTiles.Count; i++)
            {
                if (droppedTiles[i] == null)
                {
                    droppedTiles[i] = selectedFactoryTiles[tilesAddedToDroppedTiles];
                    tilesAddedToDroppedTiles++;
                }
            }

            // once dropped tile is full or all tiles have been added. remove the added tiles from selected tiles
            selectedFactoryTiles.RemoveRange(0, tilesAddedToDroppedTiles);
            // check if selected tiles still contains items.
            Trace.WriteLine("leftover dropped tiles: " + selectedFactoryTiles.Count);
        }

        internal bool TryToAddTileToDroppedTiles(Tile tileToAdd)
        {
            // first find the first null index and start adding tiles there
            int firstFreeIndex = Array.FindIndex(droppedTiles, t => t == null);

            if(firstFreeIndex >= 0)
            {
                droppedTiles[firstFreeIndex] = tileToAdd;
                return true;
            }
            return false;
        }

        internal void CalculateProductionTileScores(TileCollections tileCollections)
        {
            int[] returnList = new int[GameConstants.MAIN_TILES_LENGTH];

            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                // Move tiles out of productionTiles
                if (Array.IndexOf(productionTiles[i], null) < 0)
                {
                    int wallTileSlotIndex = 0;
                    for (int j = 0; j < productionTiles[i].Length; j++)
                    {
                        if (j == 0)
                        {
                            wallTileSlotIndex = GameConstants.GetWallTilePatternIndex(i, productionTiles[i][j].TileType);
                            wallTiles[i, wallTileSlotIndex] = productionTiles[i][j].DeepClone(i, wallTileSlotIndex);
                        }
                        else
                        {
                            tileCollections.tileBin.Add(productionTiles[i][j].DeepClone());
                        }
                        productionTiles[i][j] = null;
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
                    returnList[i] = (rowScore + columnscore - 1);
                }
                else
                {
                    returnList[i] = 0;
                }
            }

            WallTileScores = returnList;
        }

        private int CountConnectedTiles(int x, int y, int xDirection, int yDirection)
        {
            int count = 0;
            int newX = x + xDirection;
            int newY = y + yDirection;

            while (IsValidWallTile(newX, newY) && wallTiles[newX, newY] != null)
            {
                Trace.WriteLine("WallTile[" + x + ", " + y + "] is the start");
                Trace.WriteLine("WallTile[" + newX + "," + newY + "] = " + wallTiles[newX, newY].TileType);
                count++;
                newX += xDirection;
                newY += yDirection;
            }

            return count;
        }

        internal void CalculateDroppedTileScores(TileCollections tileCollections)
        {
            int totalScore = 0;
            for (int i = 0; i < GameConstants.DROPPED_TILE_LENGTH; i++)
            {
                if (droppedTiles[i] != null)
                {
                    totalScore += GameConstants.DROPPED_TILE_COSTS[i];
                    tileCollections.tileBin.Add(droppedTiles[i].DeepClone());
                }
                droppedTiles[i] = null;
            }
            DroppedTileScore = totalScore;
        }

        private bool IsValidWallTile(int x, int y)
        {
            return x >= 0 && x < wallTiles.GetLength(0) && y >= 0 && y < wallTiles.GetLength(1);
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
                    if (WallTiles[i,j] != null)
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
    }
}
