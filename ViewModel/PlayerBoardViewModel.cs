using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;

namespace WPF_Azul.ViewModel
{
    public class PlayerBoardViewModel : ViewModelBase
    {
        private ObservableCollection<ValidProductionTile> _validProductionTilesPlayer1;

        public ObservableCollection<ValidProductionTile> ValidProductionTilesPlayer1
        {
            get { return _validProductionTilesPlayer1; }
            set
            {
                _validProductionTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ValidProductionTile> _validProductionTilesPlayer2;

        public ObservableCollection<ValidProductionTile> ValidProductionTilesPlayer2
        {
            get { return _validProductionTilesPlayer2; }
            set
            {
                _validProductionTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ValidProductionTile _activatedDroppedPlayer1Tiles;

        public ValidProductionTile ActivatedDroppedPlayer1Tiles
        {
            get { return _activatedDroppedPlayer1Tiles; }
            set
            {
                _activatedDroppedPlayer1Tiles = value;
                OnPropertyChanged();
            }
        }

        private ValidProductionTile _activatedDroppedPlayer2Tiles;

        public ValidProductionTile ActivatedDroppedPlayer2Tiles
        {
            get { return _activatedDroppedPlayer2Tiles; }
            set
            {
                _activatedDroppedPlayer2Tiles = value;
                OnPropertyChanged();
            }
        }

        private List<List<Color>> _wallPattern;
        public List<List<Color>> WallPattern
        {
            get { return _wallPattern; }
            set { _wallPattern = value; }
        }

        private ObservableCollection<ObservableCollection<Tile>> _wallTilesPlayer1;

        public ObservableCollection<ObservableCollection<Tile>> WallTilesPlayer1
        {
            get { return _wallTilesPlayer1; }
            set
            {
                _wallTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> _wallTilesPlayer2;

        public ObservableCollection<ObservableCollection<Tile>> WallTilesPlayer2
        {
            get { return _wallTilesPlayer2; }
            set
            {
                _wallTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> _productionTilesPlayer1;

        public ObservableCollection<ObservableCollection<Tile>> ProductionTilesPlayer1
        {
            get { return _productionTilesPlayer1; }
            set
            {
                _productionTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> _productionTilesPlayer2;

        public ObservableCollection<ObservableCollection<Tile>> ProductionTilesPlayer2
        {
            get { return _productionTilesPlayer2; }
            set
            {
                _productionTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> _droppedTilesPlayer1;

        public ObservableCollection<Tile> DroppedTilesPlayer1
        {
            get { return _droppedTilesPlayer1; }
            set
            {
                _droppedTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> _droppedTilesPlayer2;

        public ObservableCollection<Tile> DroppedTilesPlayer2
        {
            get { return _droppedTilesPlayer2; }
            set
            {
                _droppedTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> _wallTileScoresPlayer1;

        public ObservableCollection<int> WallTileScoresPlayer1
        {
            get { return _wallTileScoresPlayer1; }
            set
            {
                _wallTileScoresPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private int _droppedTileScoresPlayer1;

        public int DroppedTileScoresPlayer1
        {
            get { return _droppedTileScoresPlayer1; }
            set
            {
                _droppedTileScoresPlayer1 = value;
                OnPropertyChanged();
            }
        }

        // These values do not change so they should not need to use OnProperyChanged()
        private int[] _droppedTileValues;

        public int[] DroppedTileValues
        {
            get { return _droppedTileValues; }
            set
            {
                _droppedTileValues = value;
                OnPropertyChanged();
            }
        }

        private int _totalScorePlayer1;

        public int TotalScorePlayer1
        {
            get { return _totalScorePlayer1; }
            set
            {
                _totalScorePlayer1 = value;
                OnPropertyChanged();
            }
        }

        public PlayerBoardViewModel()
        {
            _wallPattern = InitWallPattern();
            _productionTilesPlayer1 = InitPlayerProductionTiles();
            _productionTilesPlayer2 = InitPlayerProductionTiles();

            _wallTilesPlayer1 = InitWallTiles();
            _wallTilesPlayer2 = InitWallTiles();

            _droppedTilesPlayer1 = InitPlayerDroppedTiles();
            _droppedTilesPlayer2 = InitPlayerDroppedTiles();

            _wallTileScoresPlayer1 = new ObservableCollection<int>();
            _droppedTileScoresPlayer1 = 0;
            _totalScorePlayer1 = 0;
            _droppedTileValues = GameConstants.DROPPED_TILE_COSTS;

            _validProductionTilesPlayer1 = InitValidProductionTiles();
            _validProductionTilesPlayer2 = InitValidProductionTiles();
            _activatedDroppedPlayer1Tiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX, false);
            _activatedDroppedPlayer2Tiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX, false);
        }
    }
}
