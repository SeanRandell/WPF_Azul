using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WPF_Azul.Model;

namespace WPF_Azul.ViewModel
{
    public class PlayerBoardViewModel : ViewModelBase
    {
        private ObservableCollection<ValidProductionTile> _validProductionTiles;

        public ObservableCollection<ValidProductionTile> ValidProductionTiles
        {
            get { return _validProductionTiles; }
            set
            {
                _validProductionTiles = value;
                OnPropertyChanged();
            }
        }

        private ValidProductionTile _activatedDroppedTiles;

        public ValidProductionTile ActivatedDroppedTiles
        {
            get { return _activatedDroppedTiles; }
            set
            {
                _activatedDroppedTiles = value;
                OnPropertyChanged();
            }
        }

        private List<List<Color>> _wallPattern;
        public List<List<Color>> WallPattern
        {
            get { return _wallPattern; }
            set { _wallPattern = value; }
        }

        private ObservableCollection<ObservableCollection<Tile>> _wallTiles;

        public ObservableCollection<ObservableCollection<Tile>> WallTiles
        {
            get { return _wallTiles; }
            set
            {
                _wallTiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> _productionTiles;

        public ObservableCollection<ObservableCollection<Tile>> ProductionTiles
        {
            get { return _productionTiles; }
            set
            {
                _productionTiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> _droppedTiles;

        public ObservableCollection<Tile> DroppedTiles
        {
            get { return _droppedTiles; }
            set
            {
                _droppedTiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> _wallTileScores;

        public ObservableCollection<int> WallTileScores
        {
            get { return _wallTileScores; }
            set
            {
                _wallTileScores = value;
                OnPropertyChanged();
            }
        }

        private int _droppedTileScore;

        public int DroppedTileScore
        {
            get { return _droppedTileScore; }
            set
            {
                _droppedTileScore = value;
                OnPropertyChanged();
            }
        }

        private int _endGameScore;

        public int EndGameScore
        {
            get { return _endGameScore; }
            set
            {
                _endGameScore = value;
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

        private int _totalScore;

        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged();
            }
        }

        private Player _playerModel;

        public ICommand ProductionLineClickCommand { get; }

        public PlayerBoardViewModel(Player playerModel, ICommand ProductionLineClickCommand)
        {
            _playerModel = playerModel;
            this.ProductionLineClickCommand = ProductionLineClickCommand;
            _wallPattern = InitWallPattern();
            _productionTiles = InitPlayerProductionTiles();

            _wallTiles = InitWallTiles();

            _droppedTiles = InitPlayerDroppedTiles();

            _wallTileScores = new ObservableCollection<int>();
            _droppedTileScore = 0;
            _totalScore = 0;
            _droppedTileValues = GameConstants.DROPPED_TILE_COSTS;

            _validProductionTiles = InitValidProductionTiles();
            _activatedDroppedTiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX, false);
        }

        private ObservableCollection<ValidProductionTile> InitValidProductionTiles()
        {
            ObservableCollection<ValidProductionTile> returnList = new ObservableCollection<ValidProductionTile>();
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                ValidProductionTile newValid = new ValidProductionTile(i, false);

                returnList.Add(newValid);
            }
            return returnList;
        }

        private ObservableCollection<ObservableCollection<Tile>> InitWallTiles()
        {
            ObservableCollection<ObservableCollection<Tile>> returnlist = new ObservableCollection<ObservableCollection<Tile>>();
            for (int i = 0; i < GameConstants.MAIN_TILES_LENGTH; i++)
            {
                ObservableCollection<Tile> currentColumn = new ObservableCollection<Tile>();
                for (int j = 0; j < GameConstants.MAIN_TILES_LENGTH; j++)
                {
                    currentColumn.Add(null);
                }
                returnlist.Add(currentColumn);
            }
            return returnlist;
        }

        private ObservableCollection<ObservableCollection<Tile>> InitPlayerProductionTiles()
        {
            ObservableCollection<ObservableCollection<Tile>> returnList = new ObservableCollection<ObservableCollection<Tile>>();
            for (int i = 1; i <= GameConstants.MAIN_TILES_LENGTH; i++)
            {
                ObservableCollection<Tile> innerList = new ObservableCollection<Tile>();
                for (int j = 1; j <= i; j++)
                {
                    innerList.Add(null);
                }
                returnList.Add(innerList);
            }
            return returnList;
        }

        private ObservableCollection<Tile> InitPlayerDroppedTiles()
        {
            ObservableCollection<Tile> returnList = new ObservableCollection<Tile>();

            for (int i = 0; i < GameConstants.DROPPED_TILE_LENGTH; i++)
            {
                returnList.Add(null);
            }

            return returnList;
        }

        private List<List<Color>> InitWallPattern()
        {
            TileType[,] tileTypeArray = GameConstants.WALL_TILE_PATTERN;

            List<List<Color>> returnList = new List<List<Color>>();

            for (int i = 0; i < 5; i++)
            {
                List<Color> currentRow = new List<Color>();
                for (int j = 0; j < 5; j++)
                {
                    currentRow.Add((Color)ColorConverter.ConvertFromString(tileTypeArray[i, j].ToString()));
                }
                returnList.Add(currentRow);
            }
            return returnList;
        }

        private void UpdatePlayerScores()
        {
            TotalScore = _playerModel.score;
        }

        internal void UpdateDroppedTilesScores()
        {
            DroppedTileScore = _playerModel.PlayerBoard.DroppedTileScore;
        }

        private void UpdateWallTileScores()
        {
            UpdateCollection(_playerModel.PlayerBoard.WallTileScores, WallTileScores);
        }

        private void UpdatePlayerWallTiles()
        {
            Update2DCollection(_playerModel.PlayerBoard.WallTiles, WallTiles);
        }

        private void UpdateProductionTiles()
        {
            UpdateJaggedCollection(_playerModel.PlayerBoard.ProductionTiles, ProductionTiles);
        }

        internal void UpdateDroppedTiles()
        {
            UpdateCollection(_playerModel.PlayerBoard.DroppedTiles, DroppedTiles);
        }

        internal void UpdateViewModelFromModel()
        {
            UpdateProductionTiles();
            UpdatePlayerWallTiles();
            UpdateDroppedTiles();
        }

        internal void UpdateProductionTile(int productionTileIndex)
        {
            UpdateCollection(_playerModel.PlayerBoard.ProductionTiles[productionTileIndex], ProductionTiles[productionTileIndex]);
        }

        internal void UpdateViewModelAfterRoundEnd()
        {
            UpdateWallTileScores();
            UpdateDroppedTilesScores();
            UpdatePlayerScores();
            UpdatePlayerWallTiles();
            UpdateProductionTiles();
            UpdateDroppedTiles();
        }

        internal void UpdateViewModelAfterGameEnd()
        {
            EndGameScore = _playerModel.EndGameScore;
            UpdatePlayerScores();
        }
    }
}
