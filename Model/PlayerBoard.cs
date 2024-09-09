using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class PlayerBoard
    {
        private List<List<Tile>> wallTiles;
        public List<List<Tile>> WallTiles
        {
            get { return wallTiles; }
            set { wallTiles = value; }
        }

        private List<List<Tile>> productionTiles;
        public List<List<Tile>> ProductionTiles
        {
            get { return productionTiles; }
            set { productionTiles = value; }
        }

        private List<Tile> droppedTiles;
        public List<Tile> DroppedTiles
        {
            get { return droppedTiles; }
            set { droppedTiles = value; }
        }

        public PlayerBoard()
        {
            wallTiles = new List<List<Tile>>();
            productionTiles = new List<List<Tile>>();
            droppedTiles = new List<Tile>();

            InitDroppedTiles();
            InitWallTiles();
            InitProductionTiles();
        }

        private void InitDroppedTiles()
        {
            for (int i = 0; i < GameConstants.DROPPED_TILE_LENGTH; i++)
            {
                droppedTiles.Add(null);
                //droppedTiles.Add(new Tile(TileType.Blue));
            }
            droppedTiles[0] = new Tile(TileType.LightBlue);
            droppedTiles[1] = new Tile(TileType.Blue);
            droppedTiles[2] = new Tile(TileType.Blue);
            droppedTiles[3] = new Tile(TileType.Blue);
        }

        private void InitProductionTiles()
        {
            // Number of lists you want to create
            int numberOfLists = GameConstants.MAIN_TILES_LENGTH;

            // Generate the lists
            for (int i = 1; i <= numberOfLists; i++)
            {
                List<Tile> innerList = new List<Tile>();
                for (int j = 1; j <= i; j++)
                {
                    //innerList.Add(null);
                    innerList.Add(new Tile(TileType.Blue));
                }
                productionTiles.Add(innerList);
            }
        }

        private void InitWallTiles()
        {
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                List<Tile> innerList = new List<Tile>();
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    innerList.Add(null);
                }
                wallTiles.Add(innerList);
            }

            wallTiles[2][3] = new Tile(TileType.Blue);
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
