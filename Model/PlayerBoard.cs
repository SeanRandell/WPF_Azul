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
    }
}
