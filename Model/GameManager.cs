using System;
using System.Collections.Generic;
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

        public List<Tile> GetPlayerDroppedTiles(int playerIndex)
        {
            return GameState.players[playerIndex].PlayerBoard.DroppedTiles;
        }

        public List<Factory> GetFactories()
        {
            return GameState.Factories;
        }

        public CenterFactory GetCenterFactory()
        {
            return GameState.CenterFactory;
        }
    }
}
