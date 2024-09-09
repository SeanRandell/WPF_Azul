using System;
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

        public List<List<Tile>> GetPlayerProductionTiles(int playerIndex)
        {
            return GameState.players[playerIndex].PlayerBoard.ProductionTiles;
        }

        public List<List<Tile>> GetPlayerWallTiles(int playerIndex)
        {
            return GameState.players[playerIndex].PlayerBoard.WallTiles;
        }

        public ObservableCollection<Tile> GetPlayerDroppedTiles(int playerIndex)
        {
            ObservableCollection<Tile> returnplayerDroppedTiles = new ObservableCollection<Tile>();
            foreach (Tile tile in GameState.players[playerIndex].PlayerBoard.DroppedTiles)
            {
                returnplayerDroppedTiles.Add(tile);
            }
            return returnplayerDroppedTiles;
        }

        public List<Factory> GetFactories()
        {
            return GameState.Factories;
        }

        public List<Tile> GetCenterFactoryTiles()
        {
            return GameState.CenterFactory.FactoryTiles;
        }

        public void FactoryTileClick()
        {
            
        }
    }
}
