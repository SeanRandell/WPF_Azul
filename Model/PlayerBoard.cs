using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    class PlayerBoard
    {
        // TODO - constants see if these can go somewhere else.
        private const int mainTilesLength = 5;
        private const int floorLineMinLength = 1;
        private const int floorLineMaxLength = 5;

        private Tile[,] wallTiles;
        private Tile[][] ProductionTiles;
        private Tile[] droppedTiles;

        public PlayerBoard()
        {
            wallTiles = new Tile[mainTilesLength, mainTilesLength];
            InitWallTiles();
        }

        private void InitWallTiles()
        {
            
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
