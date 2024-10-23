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
        internal List<Player> Players { get; set; }
        internal List<Factory> Factories { get; set; }
        internal CenterFactory CenterFactory { get; set; }
        internal TileCollections TileCollections { get; set; }
        internal int ActivePlayerTurnIndex { get; set; }
        internal GamePhase GamePhase { get; set; }

        internal List<Tile> SelectedFactoryTiles { get; set; }

        internal GameState()
        {
            Players = new List<Player>();
            TileCollections = new TileCollections();
            Factories = new List<Factory>();
            CenterFactory = new CenterFactory(GameConstants.CENTER_FACTORY_INDEX);
            ActivePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;
            GamePhase = GamePhase.StartUp;

            SelectedFactoryTiles = new List<Tile>();

            InitPlayers();
            InitFactories();
            SetupFactoriesForRound();
            //TestCompletedColourScoringState();
        }

        internal void InitPlayers()
        {
            Player player1 = new Player("Player2");
            Player player2 = new Player("Player1");
            Players.Add(player1);
            Players.Add(player2);
        }

        private void InitFactories()
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                Factory currentFactory = new Factory(i);
                Factories.Add(currentFactory);
            }
        }

        internal void SetupFactoriesForRound()
        {
            foreach (Factory currentFactory in Factories)
            {
                currentFactory.SetupFactory(TileCollections);
            }

            if (CenterFactory.ContainsStartingPlayerMarker())
            {
                return;
            }

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].PlayerBoard.DroppedTiles.Any(t => t.TileType == TileType.StartingPlayerMarker))
                {
                    CenterFactory.ResetCenterFactoryForRound(Players[i].PlayerBoard.DroppedTiles);
                    break;
                }
            }

            if (!CenterFactory.ContainsStartingPlayerMarker())
            {
                CenterFactory.GetStartingPlayerTileFromTileBin(TileCollections.tileBin);
            }
        }

        internal void Player1AboutToWinState()
        {
            //make player score 100
            Players[GameConstants.STARTING_PLAYER_INDEX].Score = 100;
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            int indexOfLastTile = TileCollections.tileBag.FindIndex(x => x.TileType == TileType.Red);
            TileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
            CenterFactory.AddFactoryTile(TileCollections.tileBag[indexOfLastTile]);
            TileCollections.tileBag.RemoveAt(indexOfLastTile);

            // make sure it is player 1s turn
            ActivePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;

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
            Players[1].Score = 100;
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            for (int i = 0; i < 4; i++)
            {
                int indexOfLastTile = TileCollections.tileBag.FindIndex(x => x.TileType == TileType.Black);
                TileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
                CenterFactory.AddFactoryTile(TileCollections.tileBag[indexOfLastTile]);
                TileCollections.tileBag.RemoveAt(indexOfLastTile);
            }

            // make sure it is player 1s turn
            ActivePlayerTurnIndex = 1;

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
            Players[1].Score = 100;
            Players[0].Score = 117;
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            for (int i = 0; i < 4; i++)
            {
                int indexOfLastTile = TileCollections.tileBag.FindIndex(x => x.TileType == TileType.Black);
                TileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
                CenterFactory.AddFactoryTile(TileCollections.tileBag[indexOfLastTile]);
                TileCollections.tileBag.RemoveAt(indexOfLastTile);
            }

            // make sure it is player 1s turn
            ActivePlayerTurnIndex = 1;

            // fill the top row of the wall tiles except for the tile needed to complete a row.
            TileType ExcludingRowTile = TileType.Black;
            int rowToFill = 3;
            int columnToFill = 1;
            FillRowExceptForOne(1, rowToFill, ExcludingRowTile);
            FillColumnExceptForOne(1, columnToFill, ExcludingRowTile);
        }

        private void TestCompletedColourScoringState()
        {
            //move all tiles out of factories and move to bin except 1 in the center factory that is needed to win.
            int indexOfLastTile = TileCollections.tileBag.FindIndex(x => x.TileType == TileType.Red);
            TileCollections.tileBag[indexOfLastTile].FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
            CenterFactory.AddFactoryTile(TileCollections.tileBag[indexOfLastTile]);
            TileCollections.tileBag.RemoveAt(indexOfLastTile);

            // make sure it is player 1s turn
            ActivePlayerTurnIndex = GameConstants.STARTING_PLAYER_INDEX;

            // fill the top row of the wall tiles except for the tile needed to complete a row.
            TileType tileColourToFill = TileType.Red;
            TileType otherColourToFill = TileType.Blue;
            FillRowExceptForOne(GameConstants.STARTING_PLAYER_INDEX, 0, tileColourToFill);
            FillPlayerColourExceptForOne(GameConstants.STARTING_PLAYER_INDEX, tileColourToFill, 0);
            FillPlayerColourExceptForOne(GameConstants.STARTING_PLAYER_INDEX, otherColourToFill, -1);
        }

        private void FillPlayerColourExceptForOne(int currentPlayerIndex, TileType tileTypeToFill, int rowToExclude)
        {
            int indexOfCurrentTileTypeInTileBag;
            for (int row = 0; row < GameConstants.MAIN_TILES_LENGTH; row++)
            {
                for (int column = 0; column < GameConstants.MAIN_TILES_LENGTH; column++)
                {
                    if (row == GameConstants.GetWallTilePatternIndex(column, tileTypeToFill) && row != rowToExclude &&
                        Players[currentPlayerIndex].PlayerBoard.WallTiles[row, column] == null)
                    {
                        indexOfCurrentTileTypeInTileBag = TileCollections.tileBag.FindIndex(x => x.TileType == tileTypeToFill);
                        Players[currentPlayerIndex].PlayerBoard.WallTiles[row, column] = TileCollections.tileBag[indexOfCurrentTileTypeInTileBag];
                        TileCollections.tileBag.RemoveAt(indexOfCurrentTileTypeInTileBag);
                    }
                }
            }
        }

        private void FillRowExceptForOne(int playerIndex, int rowToFill, TileType tileTypeToExclude)
        {
            TileType currentTileType;
            int indexOfCurrentTileTypeInTileBag;
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                if (Players[playerIndex].PlayerBoard.WallTiles[rowToFill, i] != null)
                {
                    continue;
                }

                currentTileType = GameConstants.WALL_TILE_PATTERN[rowToFill, i];

                if (currentTileType == tileTypeToExclude)
                {
                    continue;
                }

                indexOfCurrentTileTypeInTileBag = TileCollections.tileBag.FindIndex(x => x.TileType == currentTileType);
                Players[playerIndex].PlayerBoard.WallTiles[rowToFill, i] = TileCollections.tileBag[indexOfCurrentTileTypeInTileBag];
                TileCollections.tileBag.RemoveAt(indexOfCurrentTileTypeInTileBag);
            }
        }

        private void FillColumnExceptForOne(int playerIndex, int columnToFill, TileType tileTypeToExclude)
        {
            TileType currentTileType;
            int indexOfCurrentTileTypeInTileBag;
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                if (Players[playerIndex].PlayerBoard.WallTiles[i, columnToFill] != null)
                {
                    continue;
                }

                currentTileType = GameConstants.WALL_TILE_PATTERN[i, columnToFill];

                if (currentTileType == tileTypeToExclude)
                {
                    continue;
                }

                indexOfCurrentTileTypeInTileBag = TileCollections.tileBag.FindIndex(x => x.TileType == currentTileType);
                Players[playerIndex].PlayerBoard.WallTiles[i, columnToFill] = TileCollections.tileBag[indexOfCurrentTileTypeInTileBag];
                TileCollections.tileBag.RemoveAt(indexOfCurrentTileTypeInTileBag);
            }
        }

        internal void ResetGame()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].ResetPlayerScore();

                Players[i].PlayerBoard.ClearWallTilesForGameEnd(TileCollections);

                Players[i].PlayerBoard.ClearProductionTilesForGameEnd(TileCollections);

                Players[i].PlayerBoard.ClearDroppedTilesForGameEnd(TileCollections);

                for (int j = 0; j < Players[i].PlayerBoard.WallTileScores.Count; j++)
                {
                    Players[i].PlayerBoard.WallTileScores[j] = 0;
                }

                Players[i].PlayerBoard.DroppedTileScore = 0;
                Players[i].EndGameScore = 0;
            }

            CenterFactory.GetStartingPlayerTileFromTileBin(TileCollections.tileBin);

            TileCollections.ResetBagAndBinForNewGame();

            SetupFactoriesForRound();
        }

        internal void ResetGameFromAnyState()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].ResetPlayerScore();

                Players[i].PlayerBoard.ClearWallTilesForGameEnd(TileCollections);

                Players[i].PlayerBoard.ClearProductionTilesForGameEnd(TileCollections);

                Players[i].PlayerBoard.ClearDroppedTilesForGameEnd(TileCollections);

                for (int j = 0; j < Players[i].PlayerBoard.WallTileScores.Count; j++)
                {
                    Players[i].PlayerBoard.WallTileScores[j] = 0;
                }

                Players[i].PlayerBoard.DroppedTileScore = 0;
                Players[i].EndGameScore = 0;
            }

            for (int i = 0; i < Factories.Count; i++)
            {
                Factories[i].BinAllTiles(TileCollections);
            }

            if (!CenterFactory.ContainsStartingPlayerMarker())
            {
                CenterFactory.GetStartingPlayerTileFromTileBin(TileCollections.tileBin);
            }

            TileCollections.ResetBagAndBinForNewGame();

            SetupFactoriesForRound();
        }
    }
}
