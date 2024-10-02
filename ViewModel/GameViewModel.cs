﻿using System;
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

        //private readonly PlayerBoard player1Board;
        //private readonly PlayerBoard player2Board;

        //TODO - For all lists being used. Change them to an observableCollection if the view needs to be changed.
        //TODO - These are temp list for configuring the view to viewmodel interaction. These should be removed if there is a more elequount way of doing this
        // TODO - Change to jsut use the wallpattern inside a player. See if an internal colour variable can be used.

        /* TODO - production tile selection
         * Have an arraylist containing each production tile as a row of clickable something?
         * Not sure what this object should be yet. Maybe just a new object?
            Defined them in the viewmodel
            clear after production tile click
            populate after factory tile click and getting which ones are valid from the gamemanager and models.
        Create in xaml with the user control being either a green arrow that you can click on or a green arrow with green boxes that surround the selectable production tiles
         */

        private TileType _selectedTileType;

        public TileType SelectedTileType
        {
            get { return _selectedTileType; }
            set { _selectedTileType = value; }
        }

        private int _selectedFactoryIndex;


        private ValidProductionTile _activatedDroppedTileObject;

        public ValidProductionTile ActivatedDroppedTileObject
        {
            get { return _activatedDroppedTileObject; }
            set { _activatedDroppedTileObject = value; }
        }


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
            set { _activatedDroppedPlayer1Tiles = value;
                OnPropertyChanged();
            }
        }

        private ValidProductionTile _activatedDroppedPlayer2Tiles;

        public ValidProductionTile ActivatedDroppedPlayer2Tiles
        {
            get { return _activatedDroppedPlayer2Tiles; }
            set { _activatedDroppedPlayer2Tiles = value;
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

        //private Color defaultTileSlotColour;

        //public Color DefaultTileSlotColour
        //{
        //    get { return defaultTileSlotColour; }
        //}

        public GameViewModel(GameManager gameManager, NavigationStore navigationStore)
        {
            //this.navigationStore = navigationStore;
            //defaultTileSlotColour = (Color)ColorConverter.ConvertFromString("Transparent");
            _selectedTileType = TileType.Blue; // default value
            _selectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            _gameManager = gameManager;
            MainMenuCommand = new MainMenuCommand(navigationStore);
            FactoryTileClickCommand = new FactoryTileClickCommand(_gameManager, this);
            ProductionLineClickCommand = new ProductionLineClickCommand(this);
            UndoFactoryTileClick = new UndoFactoryTileClick(this);
            _wallPattern = InitWallPattern();
            _productionTilesPlayer1 = InitPlayerProductionTiles();
            _productionTilesPlayer2 = InitPlayerProductionTiles();

            _wallTilesPlayer1 = InitWallTiles();
            _wallTilesPlayer2 = InitWallTiles();

            _droppedTilesPlayer1 = InitPlayerDroppedTiles();
            _droppedTilesPlayer2 = InitPlayerDroppedTiles();

            _factories = InitFactories();
            _centerFactoryTiles = new ObservableCollection<Tile>();


            _validProductionTilesPlayer1 = InitValidProductionTiles();
            _validProductionTilesPlayer2 = InitValidProductionTiles();
            _activatedDroppedPlayer1Tiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX, false);
            _activatedDroppedPlayer2Tiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX, false);
            _activatedDroppedTileObject = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX, false);
            UpdateViewModelFromModel();
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
            UpdateFactory(_selectedFactoryIndex);
            UpdateCenterFactory();
            UpdateProductionTile(productionTileIndex);
            _gameManager.UpdateDroppedTiles(DroppedTilesPlayer1);

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
            //factories = _gameManager.GetFactories();
            //centerFactoryTiles = _gameManager.GetCenterFactoryTiles();
            _gameManager.UpdateFactories(_factories);
            UpdateCenterFactory();
        }

        private void UpdateCenterFactory()
        {
            CenterFactoryTiles = _gameManager.UpdateCenterFactoryTiles();
        }

        private void UpdatePlayerWallTiles()
        {
            //wallTilesPlayer1 = _gameManager.GetPlayerWallTiles(GameConstants.STARTING_PLAYER_INDEX);
            //wallTilesPlayer2 = _gameManager.GetPlayerWallTiles(GameConstants.PLAYER_TWO_INDEX);
            _gameManager.UpdatePlayerWallTiles(WallTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            _gameManager.UpdatePlayerWallTiles(WallTilesPlayer2, GameConstants.PLAYER_TWO_INDEX);
        }

        private void UpdateProductionTiles()
        {
            //productionTilesPlayer1 = _gameManager.GetPlayerProductionTiles(GameConstants.STARTING_PLAYER_INDEX);
            //productionTilesPlayer2 = _gameManager.GetPlayerProductionTiles(GameConstants.PLAYER_TWO_INDEX);
            _gameManager.UpdatePlayerProductionTiles(ProductionTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            _gameManager.UpdatePlayerProductionTiles(ProductionTilesPlayer2, GameConstants.PLAYER_TWO_INDEX);
        }

        private void UpdateDroppedTiles()
        {
            //droppedTilesPlayer1 = _gameManager.GetPlayerDroppedTiles(GameConstants.STARTING_PLAYER_INDEX);
            //droppedTilesPlayer1 = _gameManager.GetPlayerDroppedTiles(GameConstants.PLAYER_TWO_INDEX);
            _gameManager.UpdatePlayerDroppedTiles(DroppedTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            _gameManager.UpdatePlayerDroppedTiles(DroppedTilesPlayer1, GameConstants.PLAYER_TWO_INDEX);
        }

        public void ProductionLineSelected(int productionTileIndex)
        {
            // TODO - rectoar these into a list that is accessed by the player turn index
            // reset valid production lines
            if(_gameManager.GetCurrentPlayerTurn() == GameConstants.STARTING_PLAYER_INDEX)
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
            _gameManager.ProductionTileSelected(productionTileIndex, _selectedTileType, _selectedFactoryIndex);
            Trace.WriteLine("Production line index: "+ productionTileIndex);
            //ClearObservablesCollections();
            UpdateViewModelAfterPlayerTurn(productionTileIndex);
            OnPropertyChanged();
            for (int i = 0; i < _productionTilesPlayer1[productionTileIndex].Count; i++)
            {
                if (_productionTilesPlayer1[productionTileIndex][i] != null)
                {
                    Trace.WriteLine("Production line index: " + productionTileIndex + " Equals = " + _productionTilesPlayer1[productionTileIndex][i].TileType);
                } else
                {
                    Trace.WriteLine("Production line index: " + productionTileIndex + " Equals = null");
                }
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

            // pass theses details to gamemanager.
            List<int> validProductionLineIndexes = _gameManager.GetValidProductionTiles(this._selectedTileType);
            foreach (int validIndex in validProductionLineIndexes)
            {
                //validProductionTilesPlayer1[validIndex] = new ValidProductionTile(validIndex,Visibility.Visible);
                _validProductionTilesPlayer1[validIndex].IsEnabled = true;
                Trace.WriteLine(validIndex);
            }
        }

        internal void UndoFactoryTileSelected()
        {
            foreach (ValidProductionTile productionTile in _validProductionTilesPlayer1)
            {
                productionTile.IsEnabled = false;
                _selectedFactoryIndex = GameConstants.TILE_NOT_IN_LIST_INDEX;
            }
        }
    }
}
