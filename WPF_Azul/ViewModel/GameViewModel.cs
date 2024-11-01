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
using WPF_Azul.View.UserControls;
using System.Reflection.Metadata;

namespace WPF_Azul.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private readonly GameManager _gameManager;
        private readonly NavigationStore _navigationStore;

        public ICommand OpenGameMenuCommand => new RelayCommand(execute => OpenGameMenu());

        public ICommand CloseGameMenuCommand => new RelayCommand(execute => CloseGameMenu());

        public ICommand MainMenuCommand => new RelayCommand(execute => NavigateToMainMenu());

        public ICommand FactoryTileClickCommand => new RelayCommand(selectedTile => FactoryTileSelected(selectedTile));

        public ICommand ProductionLineClickCommand => new RelayCommand(selectedProductionLine => ProductionLineSelected(selectedProductionLine));

        public ICommand UndoFactoryTileClick => new RelayCommand(execute => UndoFactoryTileSelected());

        public ICommand HowToPlayButtonCommand => new RelayCommand(execute => ShowHowToPlayModal());

        public ICommand HideHowToPlayCommand => new RelayCommand(execute => HideHowToPlayModal());


        public RelayCommand OpenRestartCommand => new RelayCommand(execute => OpenRestartMenu());
        public RelayCommand RestartGameCommand => new RelayCommand(execute => ConfirmRestartGameCommand());

        public RelayCommand CancelRestartGameCommand => new RelayCommand(execute => CloseRestartMenu());

        public RelayCommand ReplayGameButtonCommand => new RelayCommand(execute => ResetGame());

        private TileType _selectedTileType;

        private ObservableCollection<Tile> _selectedFactoryTiles;

        public ObservableCollection<Tile> SelectedFactoryTiles
        {
            get { return _selectedFactoryTiles; }
            set
            {
                _selectedFactoryTiles = value;
                OnPropertyChanged();
            }
        }

        private int SelectedFactoryIndex;

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

        private Tile _startingPlayerTile;

        public Tile StartingPlayerTile
        {
            get { return _startingPlayerTile; }
            set
            {
                _startingPlayerTile = value;
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

        private string _endGameText;

        public string EndGameText
        {
            get { return _endGameText; }
            set
            {
                _endGameText = value;
                OnPropertyChanged();
            }
        }

        private bool _gameHasEnded;

        public bool GameHasEnded
        {
            get { return _gameHasEnded; }
            set
            {
                _gameHasEnded = value;
                OnPropertyChanged();
            }
        }

        private bool _isGameEndDraw;

        public bool IsGameEndDraw
        {
            get { return _isGameEndDraw; }
            set
            {
                _isGameEndDraw = value;
                OnPropertyChanged();
            }
        }

        private bool _isRestartMenuOpen;

        public bool IsRestartMenuOpen
        {
            get { return _isRestartMenuOpen; }
            set
            {
                _isRestartMenuOpen = value;
                OnPropertyChanged();
            }
        }

        private bool _isHowToPlayModalOpen;

        public bool IsHowToPlayModalOpen
        {
            get { return _isHowToPlayModalOpen; }
            set
            {
                _isHowToPlayModalOpen = value;
                OnPropertyChanged();
            }
        }

        private bool _isGameMenuOpen;

        public bool IsGameMenuOpen
        {
            get { return _isGameMenuOpen; }
            set
            {
                _isGameMenuOpen = value;
                OnPropertyChanged();
            }
        }

        internal GameViewModel(GameManager gameManager, NavigationStore navigationStore)
        {
            _selectedTileType = TileType.Blue; // default value
            _selectedFactoryTiles = new ObservableCollection<Tile>();
            _isFactoryTileSelected = false;
            SelectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            _gameManager = gameManager;
            _navigationStore = navigationStore;

            _playerViewModels = new List<PlayerBoardViewModel>();
            InitPlayerViewModels();
            _player1BoardViewModel = PlayerViewModels[GameConstants.STARTING_PLAYER_INDEX];
            _player2BoardViewModel = PlayerViewModels[1];

            _factories = InitFactories();
            _centerFactoryTiles = new ObservableCollection<Tile>();
            _startingPlayerTile = _gameManager.GameState.CenterFactory._startingPlayerTile;

            _endGameText = "";
            _gameHasEnded = false;
            _isGameEndDraw = false;

            _isRestartMenuOpen = false;
            _isHowToPlayModalOpen = false;
            _isGameMenuOpen = false;

            _debugTileBagText = UpdateDebugTileBagText();
            _debugTileBinText = UpdateDebugTileBinText();

            UpdateViewModelFromModel();
        }

        private void InitPlayerViewModels()
        {
            for (int i = 0; i < GameConstants.DEFAULT_PLAYER_COUNT; i++)
            {
                PlayerViewModels.Add(new PlayerBoardViewModel(_gameManager.GetPlayer(i), ProductionLineClickCommand, i));
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> InitFactories()
        {
            ObservableCollection<ObservableCollection<Tile>> returnList = new ObservableCollection<ObservableCollection<Tile>>();

            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                returnList.Add(new ObservableCollection<Tile>());
            }

            return returnList;
        }

        internal void FactoryTileSelected(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            Tile selectedFactoryTile = (Tile)parameter;

            //first get factory index and the tile type from the clicked on tile
            SelectedFactoryIndex = selectedFactoryTile.FactoriesIndex;
            _selectedTileType = selectedFactoryTile.TileType;
            _gameManager.UpdateSelectedFactoryTiles(_selectedTileType, SelectedFactoryIndex);
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

            UpdateSelectedFactoryTiles();

            if (SelectedFactoryIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                UpdateCollection(_gameManager.GameState.CenterFactory.FactoryTiles, CenterFactoryTiles);
            }
            else
            {
                UpdateCollection(_gameManager.GameState.Factories[SelectedFactoryIndex].FactoryTiles, Factories[SelectedFactoryIndex]);
            }
        }

        internal void UndoFactoryTileSelected()
        {
            if (!IsFactoryTileSelected)
            {
                return;
            }

            Trace.WriteLine("Undo Command triggered");

            foreach (ValidProductionTile productionTile in PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ValidProductionTiles)
            {
                productionTile.IsEnabled = false;
            }

            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ActivatedDroppedTiles.IsEnabled = false;
            IsFactoryTileSelected = false;
            _gameManager.PlaceSelectedFactoryTilesBack(SelectedFactoryIndex);

            UpdateSelectedFactoryTiles();
            if (SelectedFactoryIndex == GameConstants.CENTER_FACTORY_INDEX)
            {
                UpdateCollection(_gameManager.GameState.CenterFactory.FactoryTiles, CenterFactoryTiles);
            }
            else
            {
                UpdateCollection(_gameManager.GameState.Factories[SelectedFactoryIndex].FactoryTiles, Factories[SelectedFactoryIndex]);
            }

            SelectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
        }

        internal void ProductionLineSelected(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            ValidProductionTile clickedProductionTile = (ValidProductionTile)parameter;
            int productionTileIndex = clickedProductionTile.ProductionTileIndex;

            // reset valid production lines
            foreach (ValidProductionTile validIndexes in PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ValidProductionTiles)
            {
                validIndexes.IsEnabled = false;
            }

            IsFactoryTileSelected = false;
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].ActivatedDroppedTiles.IsEnabled = false;

            _gameManager.ProductionTileSelected(productionTileIndex, SelectedFactoryIndex);

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
            if (_gameManager.IsRoundOver() || _gameManager.IsGameOver())
            {
                // if yes model has already intiated end of round scoring calculations, refreshed playerboards and factories.

                // viewmodel updates
                for (int i = 0; i < PlayerViewModels.Count; i++)
                {
                    PlayerViewModels[i].UpdateViewModelAfterRoundEnd();
                }

                // check for game over
                if (_gameManager.IsGameOver())
                {
                    for (int i = 0; i < PlayerViewModels.Count; i++)
                    {
                        PlayerViewModels[i].UpdateViewModelAfterGameEnd();
                    }

                    switch (_gameManager.GameState.GamePhase)
                    {
                        case GamePhase.Player1Wins:
                            UpdateViewModelForPlayerWin(GameConstants.STARTING_PLAYER_INDEX);
                            break;
                        case GamePhase.Player2Wins:
                            UpdateViewModelForPlayerWin(1);
                            break;
                        case GamePhase.Draw:
                            UpdateViewModelForDraw();
                            break;
                        default:
                            UpdateViewModelForDraw();
                            break;
                    }
                }
                else
                {
                    UpdateFactories();
                    _gameManager.StartNewRound();
                }
                DebugTileBinText = UpdateDebugTileBinText();
                DebugTileBagText = UpdateDebugTileBagText();
            }
            else
            {
                _gameManager.ChangePlayerTurn();
            }
        }

        internal void ResetGame()
        {
            GameHasEnded = false;
            IsGameEndDraw = false;

            _gameManager.RestartGame();

            UpdateViewModelForNewGame();
            UpdateFactories();
            DebugTileBagText = UpdateDebugTileBagText();
            DebugTileBinText = UpdateDebugTileBinText();
        }

        internal void ResetGameFromAnyState()
        {
            _gameManager.GameState.ResetGameFromAnyState();

            UpdateViewModelForNewGame();
            UpdateFactories();
            DebugTileBagText = UpdateDebugTileBagText();
            DebugTileBinText = UpdateDebugTileBinText();
        }

        private void UpdateViewModelForPlayerWin(int playerIndex)
        {
            GameHasEnded = true;
            EndGameText = UpdateEndGameText(playerIndex);
        }

        private void UpdateViewModelForDraw()
        {
            IsGameEndDraw = true;
            GameHasEnded = false;
            int noPlayerIndex = -1;
            EndGameText = UpdateEndGameText(noPlayerIndex);
        }


        private void UpdateViewModelFromModel()
        {
            for (int i = 0; i < PlayerViewModels.Count; i++)
            {
                PlayerViewModels[i].UpdateViewModelFromModel();
            }

            UpdateFactories();
        }

        private void UpdateViewModelAfterPlayerTurn(int productionTileIndex)
        {
            if (SelectedFactoryIndex != GameConstants.CENTER_FACTORY_INDEX)
            {
                UpdateFactory(SelectedFactoryIndex);
            }

            UpdateCenterFactory();
            UpdateSelectedFactoryTiles();

            if (productionTileIndex != GameConstants.DROPPED_TILE_ROW_INDEX)
            {
                PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].UpdateProductionTile(productionTileIndex);
            }
            PlayerViewModels[_gameManager.GetCurrentPlayerIndex()].UpdateDroppedTiles();
            DebugTileBagText = UpdateDebugTileBagText();
            DebugTileBinText = UpdateDebugTileBinText();
        }

        private string UpdateDebugTileBinText()
        {
            return "TileBag: " + _gameManager.GetDebugTileBagCount();
        }

        private string UpdateDebugTileBagText()
        {
            return "TileBin: " + _gameManager.GetDebugTileBinCount();
        }

        private string UpdateEndGameText(int winningPlayerIndex)
        {
            StringBuilder endGameStringBuilder = new StringBuilder();

            if (winningPlayerIndex < 0)
            {
                endGameStringBuilder.Append("DRAW! No one wins!");
            }
            else
            {

                endGameStringBuilder.Append("Player " + (winningPlayerIndex + 1) + " WINS!!");
            }

            return endGameStringBuilder.ToString();
        }

        private void UpdateViewModelForNewGame()
        {
            for (int i = 0; i < PlayerViewModels.Count; i++)
            {
                PlayerViewModels[i].UpdateViewModelAfterGameEnd();
                PlayerViewModels[i].UpdateViewModelAfterRoundEnd();
                PlayerViewModels[i].UpdatePlayerNameFromModel();
            }
        }

        private void UpdateFactory(int selectedFactoryIndex)
        {
            UpdateCollection(_gameManager.GameState.Factories[selectedFactoryIndex].FactoryTiles, Factories[selectedFactoryIndex]);
        }

        private void UpdateFactories()
        {
            for (int i = 0; i < GameConstants.FACTORY_COUNT; i++)
            {
                UpdateCollection(_gameManager.GameState.Factories[i].FactoryTiles, Factories[i]);
            }

            UpdateCenterFactory();
        }

        private void UpdateCenterFactory()
        {
            UpdateCollection(_gameManager.GameState.CenterFactory.FactoryTiles, CenterFactoryTiles);
            StartingPlayerTile = _gameManager.GameState.CenterFactory._startingPlayerTile;
        }

        internal void ShowHowToPlayModal()
        {
            CloseGameMenu();
            IsHowToPlayModalOpen = true;
        }

        internal void HideHowToPlayModal()
        {
            IsHowToPlayModalOpen = false;
            OpenGameMenu();
        }

        private void UpdateSelectedFactoryTiles()
        {
            UpdateCollection(_gameManager.GameState.SelectedFactoryTiles, SelectedFactoryTiles);
        }

        private void UpdatePlayerNames()
        {
            for (int i = 0; i < _gameManager.GameState.Players.Count; i++)
            {
                PlayerViewModels[i].UpdatePlayerNameFromModel();
            }
        }

        internal void StartGame(List<string> playerNames)
        {
            _gameManager.SetPlayerNames(playerNames);
            _gameManager.StartGame();
            UpdatePlayerNames();
        }

        internal void OpenRestartMenu()
        {
            CloseGameMenu();
            IsRestartMenuOpen = true;
        }

        internal void CloseRestartMenu()
        {
            OpenGameMenu();
            IsRestartMenuOpen = false;
        }

        internal void ConfirmRestartGameCommand()
        {
            IsRestartMenuOpen = false;
            ResetGameFromAnyState();
        }

        internal void NavigateToMainMenu()
        {
            // TODO - place an are you sure modal here warnng that the game will be reset.
            CloseGameMenu();
            ResetGameFromAnyState();
            _navigationStore.NavigateMainMenu();
        }

        internal void OpenGameMenu()
        {
            IsGameMenuOpen = true;
        }

        internal void CloseGameMenu()
        {
            IsGameMenuOpen = false;
        }
    }
}
