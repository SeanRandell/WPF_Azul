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

        private PlayerBoardViewModel _player1BoardViewModel;

        public PlayerBoardViewModel Player1BoardViewModel
        {
            get { return _player1BoardViewModel; }
            set
            {
                _player1BoardViewModel = value;
                OnPropertyChanged();
            }
        }

        private PlayerBoardViewModel _player2BoardViewModel;

        public PlayerBoardViewModel Player2BoardViewModel
        {
            get { return _player2BoardViewModel; }
            set
            {
                _player2BoardViewModel = value;
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
            _player1BoardViewModel = PlayerViewModels[GameConstants.STARTING_PLAYER_INDEX];
            _player2BoardViewModel = PlayerViewModels[1];

            _factories = InitFactories();
            _centerFactoryTiles = new ObservableCollection<Tile>();

            _debugTileBagText = UpdateDebugTileBagText();
            _debugTileBinText = UpdateDebugTileBinText();

            UpdateViewModelFromModel();
        }

        private void InitPlayerViewModels()
        {
            // TODO - change.
            PlayerViewModels.Add(new PlayerBoardViewModel(_gameManager.GetPlayer(GameConstants.STARTING_PLAYER_INDEX), ProductionLineClickCommand));
            PlayerViewModels.Add(new PlayerBoardViewModel(_gameManager.GetPlayer(1), ProductionLineClickCommand));
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
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].UpdateViewModelFromModel();
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
                PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].UpdateProductionTile(productionTileIndex);
            }
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].UpdateDroppedTiles();
            DebugTileBagText = UpdateDebugTileBagText();
            DebugTileBinText = UpdateDebugTileBinText();
        }

        private void UpdateFactory(int selectedFactoryIndex)
        {
            _factories[selectedFactoryIndex] = _gameManager.UpdateFactory(selectedFactoryIndex);
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

        public void ProductionLineSelected(int productionTileIndex)
        {
            // reset valid production lines
            foreach (ValidProductionTile validIndexes in PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ValidProductionTiles)
            {
                validIndexes.IsEnabled = false;
            }

            IsFactoryTileSelected = false;
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ActivatedDroppedTiles.IsEnabled = false;

            _gameManager.ProductionTileSelected(productionTileIndex, _selectedTileType, _selectedFactoryIndex);

            Trace.WriteLine("Production line index: " + productionTileIndex);

            UpdateViewModelAfterPlayerTurn(productionTileIndex);

            if (productionTileIndex != GameConstants.DROPPED_TILE_ROW_INDEX)
            {
                for (int i = 0; i < PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ProductionTiles[productionTileIndex].Count; i++)
                {
                    if (PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ProductionTiles[productionTileIndex][i] != null)
                    {
                        Trace.WriteLine("Production line index: " + productionTileIndex + " Equals = " + PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ProductionTiles[productionTileIndex][i].TileType);
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
                for (int i = 0; i < PlayerViewModels.Count; i++)
                {
                    PlayerViewModels[i].UpdateViewModelAfterRoundEnd();
                }

                UpdateFactories();
                DebugTileBinText = UpdateDebugTileBinText();
                DebugTileBagText = UpdateDebugTileBagText();
                _gameManager.StartNewRound();
            }

            _gameManager.ChangePlayerTurn();
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
                PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ValidProductionTiles[validIndex].IsEnabled = true;

                Trace.WriteLine("Valid Production Tile index: " + validIndex);
            }
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ActivatedDroppedTiles.IsEnabled = true;
        }

        internal void UndoFactoryTileSelected()
        {
            foreach (ValidProductionTile productionTile in PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ValidProductionTiles)
            {
                productionTile.IsEnabled = false;
                _selectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            }
            // ToDo change for both players
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ActivatedDroppedTiles.IsEnabled = false;
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
