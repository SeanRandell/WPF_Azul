using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.Model
{
    public class GameState
    {
        public List<Player> players;
        public List<Factory> Factories;
        public CenterFactory CenterFactory;
        public TileCollections tileCollections;
        public int activePlayerTurnIndex;

        public GameState()
        {
            players = new List<Player>();
            tileCollections = new TileCollections();
            Factories = new List<Factory>();
            CenterFactory = new CenterFactory();
            activePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;
            InitPlayers();
            InitFactories();
            SetupFactoriesForRound();
        }

        private void InitPlayers()
        {
            Player player1 = new Player("Player2");
            Player player2 = new Player("Player1");
            players.Add(player1);
            players.Add(player2);
        }

        private void InitFactories()
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                Factory currentFactory = new Factory();
                currentFactory.FactoryIndex = i;
                Factories.Add(currentFactory);
            }
        }

        public void SetupFactoriesForRound()
        {
            foreach (Factory currentFactory in Factories)
            {
                currentFactory.SetupFactory(tileCollections);
            }
            //CenterFactory.AddFactoryTile(new Tile(TileType.Red, GameConstants.TILE_NOT_ON_PLAYERBOARD_INDEX, GameConstants.TILE_NOT_ON_PLAYERBOARD_INDEX));
            //CenterFactory.AddFactoryTile(new Tile(TileType.Blue, GameConstants.TILE_NOT_ON_PLAYERBOARD_INDEX, GameConstants.TILE_NOT_ON_PLAYERBOARD_INDEX));
            //CenterFactory.AddFactoryTile(new Tile(TileType.Black, GameConstants.TILE_NOT_ON_PLAYERBOARD_INDEX, GameConstants.TILE_NOT_ON_PLAYERBOARD_INDEX));
        }
    }
}
