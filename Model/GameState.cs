using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WPF_Azul.Model
{
    internal class GameState
    {
        internal List<Player> players;
        internal List<Factory> Factories;
        internal CenterFactory CenterFactory;
        internal TileCollections tileCollections;
        internal int activePlayerTurnIndex;
        internal GamePhase _gamePhase;

        internal GameState()
        {
            players = new List<Player>();
            tileCollections = new TileCollections();
            Factories = new List<Factory>();
            CenterFactory = new CenterFactory();
            activePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;
            _gamePhase = GamePhase.StartUp;
            InitPlayers();
            InitFactories();
            SetupFactoriesForRound();
            //DrawState();
        }

        internal void InitPlayers()
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

        internal void SetupFactoriesForRound()
        {
            foreach (Factory currentFactory in Factories)
            {
                currentFactory.SetupFactory(tileCollections);
            }

            if (CenterFactory.ContainsStartingPlayerMarker())
            {
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                int firstNonNullIndex = Array.FindIndex(players[i].PlayerBoard.DroppedTiles, t => t != null);
                if (firstNonNullIndex >= 0)
                {
                    if (players[i].PlayerBoard.DroppedTiles.Any(t => t.TileType == TileType.StartingPlayerMarker))
                    {
                        CenterFactory.ResetCenterFactoryForRound(players[i].PlayerBoard.DroppedTiles);
                        break;
                    }
                }
            }

            if (!CenterFactory.ContainsStartingPlayerMarker())
            {
                CenterFactory.GetStartingPlayerTileFromTileBin(tileCollections.tileBin);
            }
        }

        internal void Player1AboutToWinState()
        {
            //make player score 100
            players[GameConstants.STARTING_PLAYER_INDEX].score = 100;
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            int indexOfLastTile = tileCollections.tileBag.FindIndex(x => x.TileType == TileType.Red);
            tileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
            CenterFactory.AddFactoryTile(tileCollections.tileBag[indexOfLastTile]);
            tileCollections.tileBag.RemoveAt(indexOfLastTile);

            // make sure it is player 1s turn
            activePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;

            // fill the top row of the wall tiles except for the tile needed to complete a row.
            TileType ExcludingRowTile = TileType.Red;
            int rowToFill = 0;
            int columnToFill = 2;
            FillRowExceptForOne(GameConstants.STARTING_PLAYER_INDEX, rowToFill, ExcludingRowTile);
            FillColumnExceptForOne(GameConstants.STARTING_PLAYER_INDEX, columnToFill, ExcludingRowTile);
        }

        internal void Player2AboutToWinState()
        {
            //make player score 100
            players[1].score = 100;
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            for (int i = 0; i < 4; i++)
            {
                int indexOfLastTile = tileCollections.tileBag.FindIndex(x => x.TileType == TileType.Black);
                tileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
                CenterFactory.AddFactoryTile(tileCollections.tileBag[indexOfLastTile]);
                tileCollections.tileBag.RemoveAt(indexOfLastTile);
            }

            // make sure it is player 1s turn
            activePlayerTurnIndex = 1;

            // fill the top row of the wall tiles except for the tile needed to complete a row.
            TileType ExcludingRowTile = TileType.Black;
            int rowToFill = 3;
            int columnToFill = 1;
            FillRowExceptForOne(1, rowToFill, ExcludingRowTile);
            FillColumnExceptForOne(1, columnToFill, ExcludingRowTile);
        }

        internal void DrawState()
        {
            //make player score 100
            players[1].score = 100;
            players[0].score = 118;
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            for (int i = 0; i < 4; i++)
            {
                int indexOfLastTile = tileCollections.tileBag.FindIndex(x => x.TileType == TileType.Black);
                tileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
                CenterFactory.AddFactoryTile(tileCollections.tileBag[indexOfLastTile]);
                tileCollections.tileBag.RemoveAt(indexOfLastTile);
            }

            // make sure it is player 1s turn
            activePlayerTurnIndex = 1;

            // fill the top row of the wall tiles except for the tile needed to complete a row.
            TileType ExcludingRowTile = TileType.Black;
            int rowToFill = 3;
            int columnToFill = 1;
            FillRowExceptForOne(1, rowToFill, ExcludingRowTile);
            FillColumnExceptForOne(1, columnToFill, ExcludingRowTile);
        }

        private void FillRowExceptForOne(int playerIndex, int rowToFill, TileType tileTypeToExclude)
        {
            TileType currentTileType;
            int indexOfCurrentTileTypeInTileBag;
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                currentTileType = GameConstants.WALL_TILE_PATTERN[rowToFill, i];

                if (currentTileType == tileTypeToExclude)
                {
                    continue;
                }

                indexOfCurrentTileTypeInTileBag = tileCollections.tileBag.FindIndex(x => x.TileType == currentTileType);
                players[playerIndex].PlayerBoard.WallTiles[rowToFill, i] = tileCollections.tileBag[indexOfCurrentTileTypeInTileBag];
                tileCollections.tileBag.RemoveAt(indexOfCurrentTileTypeInTileBag);
            }
        }

        private void FillColumnExceptForOne(int playerIndex, int columnToFill, TileType tileTypeToExclude)
        {
            TileType currentTileType;
            int indexOfCurrentTileTypeInTileBag;
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                currentTileType = GameConstants.WALL_TILE_PATTERN[i, columnToFill];

                if (currentTileType == tileTypeToExclude)
                {
                    continue;
                }

                indexOfCurrentTileTypeInTileBag = tileCollections.tileBag.FindIndex(x => x.TileType == currentTileType);
                players[playerIndex].PlayerBoard.WallTiles[i, columnToFill] = tileCollections.tileBag[indexOfCurrentTileTypeInTileBag];
                tileCollections.tileBag.RemoveAt(indexOfCurrentTileTypeInTileBag);
            }
        }
    }
}
