using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WPF_Azul.ViewModel.Commands;
using WPF_Azul.Model;
using WPF_Azul.Stores;
using WPF_Azul.View;

namespace WPF_Azul.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        //private readonly NavigationStore navigationStore;

        private readonly GameManager _gameManager;

        // TODO - move to a modal or other kind of menu
        public ICommand MainMenuCommand { get; }
        public ICommand FactoryTileClickCommand { get; }
        public ICommand ProductionLineClickCommand { get; }

        public ICommand UndoFactoryTileClick { get; }

        // TODO - Change to jsut use the wallpattern inside a player. See if an internal colour variable can be used.

        private TileType _selectedTileType;

        public TileType SelectedTileType
        {
            get { return _selectedTileType; }
            set { _selectedTileType = value; }
        }

        private int _selectedFactoryIndex;

        //private ObservableCollection<ValidProductionTile> _validProductionTilesPlayer1;

        //public ObservableCollection<ValidProductionTile> ValidProductionTilesPlayer1
        //{
        //    get { return _validProductionTilesPlayer1; }
        //    set
        //    {
        //        _validProductionTilesPlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<ValidProductionTile> _validProductionTilesPlayer2;

        //public ObservableCollection<ValidProductionTile> ValidProductionTilesPlayer2
        //{
        //    get { return _validProductionTilesPlayer2; }
        //    set
        //    {
        //        _validProductionTilesPlayer2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ValidProductionTile _activatedDroppedPlayer1Tiles;

        //public ValidProductionTile ActivatedDroppedPlayer1Tiles
        //{
        //    get { return _activatedDroppedPlayer1Tiles; }
        //    set
        //    {
        //        _activatedDroppedPlayer1Tiles = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ValidProductionTile _activatedDroppedPlayer2Tiles;

        //public ValidProductionTile ActivatedDroppedPlayer2Tiles
        //{
        //    get { return _activatedDroppedPlayer2Tiles; }
        //    set
        //    {
        //        _activatedDroppedPlayer2Tiles = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private List<List<Color>> _wallPattern;
        //public List<List<Color>> WallPattern
        //{
        //    get { return _wallPattern; }
        //    set { _wallPattern = value; }
        //}

        //private ObservableCollection<ObservableCollection<Tile>> _wallTilesPlayer1;

        //public ObservableCollection<ObservableCollection<Tile>> WallTilesPlayer1
        //{
        //    get { return _wallTilesPlayer1; }
        //    set
        //    {
        //        _wallTilesPlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<ObservableCollection<Tile>> _wallTilesPlayer2;

        //public ObservableCollection<ObservableCollection<Tile>> WallTilesPlayer2
        //{
        //    get { return _wallTilesPlayer2; }
        //    set
        //    {
        //        _wallTilesPlayer2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<ObservableCollection<Tile>> _productionTilesPlayer1;

        //public ObservableCollection<ObservableCollection<Tile>> ProductionTilesPlayer1
        //{
        //    get { return _productionTilesPlayer1; }
        //    set
        //    {
        //        _productionTilesPlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<ObservableCollection<Tile>> _productionTilesPlayer2;

        //public ObservableCollection<ObservableCollection<Tile>> ProductionTilesPlayer2
        //{
        //    get { return _productionTilesPlayer2; }
        //    set
        //    {
        //        _productionTilesPlayer2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<Tile> _droppedTilesPlayer1;

        //public ObservableCollection<Tile> DroppedTilesPlayer1
        //{
        //    get { return _droppedTilesPlayer1; }
        //    set
        //    {
        //        _droppedTilesPlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<Tile> _droppedTilesPlayer2;

        //public ObservableCollection<Tile> DroppedTilesPlayer2
        //{
        //    get { return _droppedTilesPlayer2; }
        //    set
        //    {
        //        _droppedTilesPlayer2 = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ObservableCollection<ObservableCollection<Tile>> _factories;

        public ObservableCollection<ObservableCollection<Tile>> Factories
        {
            get { return _factories; }
            set
            {
                _factories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> _centerFactoryTiles;

        public ObservableCollection<Tile> CenterFactoryTiles
        {
            get { return _centerFactoryTiles; }
            set
            {
                _centerFactoryTiles = value;
                OnPropertyChanged();
            }
        }

        //private ObservableCollection<int> _wallTileScoresPlayer1;

        //public ObservableCollection<int> WallTileScoresPlayer1
        //{
        //    get { return _wallTileScoresPlayer1; }
        //    set
        //    {
        //        _wallTileScoresPlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private int _droppedTileScoresPlayer1;

        //public int DroppedTileScoresPlayer1
        //{
        //    get { return _droppedTileScoresPlayer1; }
        //    set
        //    {
        //        _droppedTileScoresPlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //// These values do not change so they should not need to use OnProperyChanged()
        //private int[] _droppedTileValues;

        //public int[] DroppedTileValues
        //{
        //    get { return _droppedTileValues; }
        //    set
        //    {
        //        _droppedTileValues = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private int _totalScorePlayer1;

        //public int TotalScorePlayer1
        //{
        //    get { return _totalScorePlayer1; }
        //    set
        //    {
        //        _totalScorePlayer1 = value;
        //        OnPropertyChanged();
        //    }
        //}


        private string _debugTileBagText;

        public string DebugTileBagText
        {
            get { return _debugTileBagText; }
            set
            {
                _debugTileBagText = value;
                OnPropertyChanged();
            }
        }

        private string _debugTileBinText;

        public string DebugTileBinText
        {
            get { return _debugTileBinText; }
            set
            {
                _debugTileBinText = value;
                OnPropertyChanged();
            }
        }

        private bool _isFactoryTileSelected;

        public bool IsFactoryTileSelected
        {
            get { return _isFactoryTileSelected; }
            set
            {
                _isFactoryTileSelected = value;
                OnPropertyChanged();
            }
        }

        private List<PlayerBoardViewModel> _playerViewModels;

        public List<PlayerBoardViewModel> PlayerViewModels
        {
            get { return _playerViewModels; }
            set { _playerViewModels = value; }
        }


        public GameViewModel(GameManager gameManager, NavigationStore navigationStore)
        {
            _selectedTileType = TileType.Blue; // default value
            _isFactoryTileSelected = false;
            _selectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            _gameManager = gameManager;
            MainMenuCommand = new MainMenuCommand(navigationStore);
            FactoryTileClickCommand = new FactoryTileClickCommand(_gameManager, this);
            ProductionLineClickCommand = new ProductionLineClickCommand(this);
            UndoFactoryTileClick = new UndoFactoryTileClick(this);

            _playerViewModels = new List<PlayerBoardViewModel>();
            InitPlayerViewModels();

            _factories = InitFactories();
            _centerFactoryTiles = new ObservableCollection<Tile>();

            _debugTileBagText = UpdateDebugTileBagText();
            _debugTileBinText = UpdateDebugTileBinText();

            UpdateViewModelFromModel();
        }

        private void InitPlayerViewModels()
        {
            PlayerViewModels.Add(new PlayerBoardViewModel(_gameManager));
            PlayerViewModels.Add(new PlayerBoardViewModel(_gameManager));
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

        private ObservableCollection<ObservableCollection<Tile>> InitFactories()
        {
            ObservableCollection<ObservableCollection<Tile>> returnList = new ObservableCollection<ObservableCollection<Tile>>();

            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                ObservableCollection<Tile> innerList = new ObservableCollection<Tile>();
                for (int j = 0; j < GameConstants.NORMAL_FACTORY_MAX_TILES; j++)
                {
                    innerList.Add(null);
                }
                returnList.Add(innerList);
            }

            return returnList;
        }

        private void UpdateViewModelFromModel()
        {
            UpdateProductionTiles();
            UpdatePlayerWallTiles();
            UpdateDroppedTiles();
            UpdateFactories();
        }

        // TODO - Update for to work for both players
        private void UpdateViewModelAfterPlayerTurn(int productionTileIndex)
        {
            if (_selectedFactoryIndex != GameConstants.CENTER_FACTORY_INDEX)
            {
                UpdateFactory(_selectedFactoryIndex);
            }

            UpdateCenterFactory();

            if (productionTileIndex != GameConstants.DROPPED_TILE_ROW_INDEX)
            {
                UpdateProductionTile(productionTileIndex);
            }
            _gameManager.UpdateDroppedTiles(DroppedTilesPlayer1);
            DebugTileBagText = UpdateDebugTileBagText();
            DebugTileBinText = UpdateDebugTileBinText();
        }

        private void UpdateProductionTile(int productionTileIndex)
        {
            _gameManager.UpdateProductionTiles(_productionTilesPlayer1[productionTileIndex], productionTileIndex);
        }

        private void UpdateFactory(int selectedFactoryIndex)
        {
            _factories[selectedFactoryIndex] = _gameManager.UpdateFactory(selectedFactoryIndex);
        }

        private void ClearObservablesCollections()
        {
            _productionTilesPlayer1.Clear();
            _productionTilesPlayer2.Clear();

            _wallTilesPlayer1.Clear();
            _wallTilesPlayer2.Clear();

            _droppedTilesPlayer1.Clear();
            _droppedTilesPlayer2.Clear();

            _factories.Clear();
            _centerFactoryTiles.Clear();
        }

        private void UpdateFactories()
        {
            _gameManager.UpdateFactories(_factories);
            UpdateCenterFactory();
        }

        private void UpdateCenterFactory()
        {
            CenterFactoryTiles = _gameManager.UpdateCenterFactoryTiles();
        }

        private void UpdatePlayerWallTiles()
        {
            _gameManager.UpdatePlayerWallTiles(WallTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            //_gameManager.UpdatePlayerWallTiles(WallTilesPlayer2, GameConstants.PLAYER_TWO_INDEX);
        }

        private void UpdateProductionTiles()
        {
            _gameManager.UpdatePlayerProductionTiles(ProductionTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            //_gameManager.UpdatePlayerProductionTiles(ProductionTilesPlayer2, GameConstants.PLAYER_TWO_INDEX);
        }

        private void UpdateDroppedTiles()
        {
            _gameManager.UpdatePlayerDroppedTiles(DroppedTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            //_gameManager.UpdatePlayerDroppedTiles(DroppedTilesPlayer1, GameConstants.PLAYER_TWO_INDEX);
        }

        public void ProductionLineSelected(int productionTileIndex)
        {
            // TODO - rectoar these into a list that is accessed by the player turn index
            // reset valid production lines
            if (_gameManager.GetCurrentPlayerTurn() == GameConstants.STARTING_PLAYER_INDEX)
            {
                foreach (ValidProductionTile validIndexes in _validProductionTilesPlayer1)
                {
                    validIndexes.IsEnabled = false;
                }
            }
            else
            {
                foreach (ValidProductionTile validIndexes in _validProductionTilesPlayer2)
                {
                    validIndexes.IsEnabled = false;
                }
            }
            IsFactoryTileSelected = false;
            ActivatedDroppedPlayer1Tiles.IsEnabled = false;

            _gameManager.ProductionTileSelected(productionTileIndex, _selectedTileType, _selectedFactoryIndex);

            Trace.WriteLine("Production line index: " + productionTileIndex);

            UpdateViewModelAfterPlayerTurn(productionTileIndex);

            if (productionTileIndex != GameConstants.DROPPED_TILE_ROW_INDEX)
            {
                for (int i = 0; i < _productionTilesPlayer1[productionTileIndex].Count; i++)
                {
                    if (_productionTilesPlayer1[productionTileIndex][i] != null)
                    {
                        Trace.WriteLine("Production line index: " + productionTileIndex + " Equals = " + _productionTilesPlayer1[productionTileIndex][i].TileType);
                    }
                    else
                    {
                        Trace.WriteLine("Production line index: " + productionTileIndex + " Equals = null");
                    }
                }
            }

            CheckForRoundEnd();
        }

        private void CheckForRoundEnd()
        {
            // model checks for round end. possibly do in and after production tile selected method.
            _gameManager.CheckForRoundEndAndProcess();

            // gameviewmodel checks for gamestate change. Is there a gamestate enum?
            if (_gameManager.IsRoundOver())
            {
                // if yes model has already intiated end of round scoring calculations, refreshed playerboards and factories.

                // viewmodel updates
                UpdateWallTileScores();
                UpdateDroppedTilesScores();
                UpdatePlayerScores();
                UpdatePlayerWallTiles();
                UpdateProductionTiles();
                UpdateDroppedTiles();
                UpdateFactories();
                DebugTileBinText = UpdateDebugTileBinText();
                DebugTileBagText = UpdateDebugTileBagText();
                _gameManager.StartNewRound();
            }
        }

        private void UpdatePlayerScores()
        {
            TotalScorePlayer1 = _gameManager.GetTotalPlayerScore();
        }

        private void UpdateDroppedTilesScores()
        {
            DroppedTileScoresPlayer1 = _gameManager.GetPlayerDroppedTileScore();
        }

        private void UpdateWallTileScores()
        {
            List<int> newScores = _gameManager.GetPlayerWallScores();
            WallTileScoresPlayer1.Clear();
            for (int i = 0; i < newScores.Count; i++)
            {
                WallTileScoresPlayer1.Add(newScores[i]);
            }
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

        public void FactoryTileSelected(TileType selectedTileType, int factoriesIndex)
        {
            //first get factory index and the tile type from the clicked on tile
            _selectedFactoryIndex = factoriesIndex;
            _selectedTileType = selectedTileType;
            IsFactoryTileSelected = true;

            // pass theses details to gamemanager.
            List<int> validProductionLineIndexes = _gameManager.GetValidProductionTiles(_selectedTileType);
            foreach (int validIndex in validProductionLineIndexes)
            {
                //validProductionTilesPlayer1[validIndex] = new ValidProductionTile(validIndex,Visibility.Visible);
                ValidProductionTilesPlayer1[validIndex].IsEnabled = true;

                Trace.WriteLine(validIndex);
            }
            ActivatedDroppedPlayer1Tiles.IsEnabled = true;
        }

        internal void UndoFactoryTileSelected()
        {
            foreach (ValidProductionTile productionTile in _validProductionTilesPlayer1)
            {
                productionTile.IsEnabled = false;
                _selectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            }
            // ToDo change for both players
            ActivatedDroppedPlayer1Tiles.IsEnabled = false;
            IsFactoryTileSelected = false;
        }

        private string UpdateDebugTileBinText()
        {
            return "TileBag: " + _gameManager.GetDebugTileBagCount();
        }

        private string UpdateDebugTileBagText()
        {
            return "TileBag: " + _gameManager.GetDebugTileBinCount();
        }
    }
}
