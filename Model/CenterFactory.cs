using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPF_Azul.Model
{
    internal class CenterFactory : FactoryBase
    {
        internal Tile _startingPlayerTile { get; set; }

        internal CenterFactory(int factoryIndex) : base(factoryIndex)
        {
            _startingPlayerTile = new Tile(TileType.StartingPlayerMarker);
            _startingPlayerTile.FactoriesIndex = GameConstants.CENTER_FACTORY_INDEX;
        }

        internal void ResetCenterFactoryForNewGame(TileCollections tileCollections)
        {
            tileCollections.tileBag.AddRange(FactoryTiles);
            FactoryTiles.Clear();
        }

        internal void ResetCenterFactoryForRound(List<Tile> droppedTiles)
        {
            for (int i = 0; i < droppedTiles.Count; i++)
            {
                if (droppedTiles[i] != null)
                {
                    continue;
                }

                if (droppedTiles[i].TileType == TileType.StartingPlayerMarker)
                {
                    _startingPlayerTile = droppedTiles[i];
                    droppedTiles.RemoveAt(i);
                }
            }
        }

        internal bool ContainsStartingPlayerMarker()
        {
            if (_startingPlayerTile != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void CheckAndProcessIfFirstSelected(PlayerBoard playerBoard, TileCollections tileCollections)
        {
            if (ContainsStartingPlayerMarker())
            {
                if (playerBoard.TryToAddTileToDroppedTiles(_startingPlayerTile))
                {
                    _startingPlayerTile = null;
                }
                else
                {
                    tileCollections.tileBin.Add(_startingPlayerTile);
                }
            }
        }

        internal void GetStartingPlayerTileFromTileBin(List<Tile> tileBin)
        {
            int startingPlayerTileIndex = tileBin.FindIndex(tile => tile.TileType == TileType.StartingPlayerMarker);
            _startingPlayerTile = tileBin[startingPlayerTileIndex];
            tileBin.RemoveAt(startingPlayerTileIndex);
        }
    }
}
