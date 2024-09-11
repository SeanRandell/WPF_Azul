using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PlayerBoard()
        {
            wallTiles = new Tile[GameConstants.MAIN_TILES_LENGTH, GameConstants.MAIN_TILES_LENGTH];
            productionTiles = new Tile[GameConstants.MAIN_TILES_LENGTH][];
            droppedTiles = new Tile[GameConstants.DROPPED_TILE_LENGTH];

            InitDroppedTiles();
            InitWallTiles();
            InitProductionTiles();
        }

        private void InitDroppedTiles()
        {
            for (int i = 0; i < GameConstants.DROPPED_TILE_LENGTH; i++)
            {
                droppedTiles[i] = null;
            }
            droppedTiles[0] = new Tile(TileType.LightBlue);
            droppedTiles[1] = new Tile(TileType.Blue);
            droppedTiles[2] = new Tile(TileType.Blue);
            droppedTiles[3] = new Tile(TileType.Blue);
        }

        private void InitProductionTiles()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                productionTiles[i] = new Tile[i + 1];
                for (int j = 0; j <= i; j++)
                {
                    productionTiles[i][j] = null;
                }
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

            // TODO - Remove after testing is done
            //wallTiles[2,3] = new Tile(TileType.Blue);
        }

        // Check for rows and columns
        public void GetTilePlacementScore(int tileX, int tileY)
        {
            // start counting in each direction until either hit edge of 
        }

        public void CheckForRow(int TileY)
        {

        }

        public void CheckForColumn(int tileX)
        {

        }

        // TODO - Find a way to optimise 
        public List<int> GetValidProductionTilesIndexes(TileType selectedTileType)
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
                bool isProductionLineFull = false;
                // check if the production line is full
                for (int j = 0; j < productionTiles[i].Length; j++)
                {
                    if (productionTiles[i][j] != null)
                    {
                        isProductionLineFull = true;
                    }
                    else
                    {
                        isProductionLineFull = false;
                    }
                }

                // if there space but there is already tiles in this row
                // check if there is already the same type of tile in the production line
                bool doesProductionLineMatchSelectedType = false;
                for (int j = 0; j < productionTiles[i].Length; j++)
                {
                    if(productionTiles[i][j] != null)
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

        public Tile[] GetRow(Tile[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}
