using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WPF_Azul.Commands;
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

        private ValidProductionTile activatedDroppedTileObject;

        public ValidProductionTile ActivatedDroppedTileObject
        {
            get { return activatedDroppedTileObject; }
            set { activatedDroppedTileObject = value; }
        }


        private ObservableCollection<ValidProductionTile> validProductionTilesPlayer1;

        public ObservableCollection<ValidProductionTile> ValidProductionTilesPlayer1
        {
            get { return validProductionTilesPlayer1; }
            set 
            { 
                validProductionTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ValidProductionTile> validProductionTilesPlayer2;

        public ObservableCollection<ValidProductionTile> ValidProductionTilesPlayer2
        {
            get { return validProductionTilesPlayer2; }
            set
            {
                validProductionTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ValidProductionTile activatedDroppedPlayer1Tiles;

        public ValidProductionTile ActivatedDroppedPlayer1Tiles
        {
            get { return activatedDroppedPlayer1Tiles; }
            set { activatedDroppedPlayer1Tiles = value;
                OnPropertyChanged();
            }
        }

        private ValidProductionTile activatedDroppedPlayer2Tiles;

        public ValidProductionTile ActivatedDroppedPlayer2Tiles
        {
            get { return activatedDroppedPlayer2Tiles; }
            set { activatedDroppedPlayer2Tiles = value;
                OnPropertyChanged();
            }
        }

        private List<List<Color>> wallPattern;
        public List<List<Color>> WallPattern
        {
            get { return wallPattern; }
            set { wallPattern = value; }
        }

        private ObservableCollection<ObservableCollection<Tile>> wallTilesPlayer1;

        public ObservableCollection<ObservableCollection<Tile>> WallTilesPlayer1
        {
            get { return wallTilesPlayer1; }
            set
            {
                wallTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> wallTilesPlayer2;

        public ObservableCollection<ObservableCollection<Tile>> WallTilesPlayer2
        {
            get { return wallTilesPlayer2; }
            set
            {
                wallTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> productionTilesPlayer1;

        public ObservableCollection<ObservableCollection<Tile>> ProductionTilesPlayer1
        {
            get { return productionTilesPlayer1; }
            set
            {
                productionTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> productionTilesPlayer2;

        public ObservableCollection<ObservableCollection<Tile>> ProductionTilesPlayer2
        {
            get { return productionTilesPlayer2; }
            set
            {
                productionTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> droppedTilesPlayer1;

        public ObservableCollection<Tile> DroppedTilesPlayer1
        {
            get { return droppedTilesPlayer1; }
            set
            {
                droppedTilesPlayer1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> droppedTilesPlayer2;

        public ObservableCollection<Tile> DroppedTilesPlayer2
        {
            get { return droppedTilesPlayer2; }
            set
            {
                droppedTilesPlayer2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Tile>> factories;

        public ObservableCollection<ObservableCollection<Tile>> Factories
        {
            get { return factories; }
            set
            {
                factories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tile> centerFactoryTiles;

        public ObservableCollection<Tile> CenterFactoryTiles
        {
            get { return centerFactoryTiles; }
            set
            {
                centerFactoryTiles = value;
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
            _gameManager = gameManager;
            MainMenuCommand = new MainMenuCommand(navigationStore);
            FactoryTileClickCommand = new FactoryTileClickCommand(_gameManager, this);
            ProductionLineClickCommand = new ProductionLineClickCommand(this);
            wallPattern = InitWallPattern();
            productionTilesPlayer1 = InitPlayerProductionTiles();
            productionTilesPlayer2 = InitPlayerProductionTiles();

            wallTilesPlayer1 = InitWallTiles();
            wallTilesPlayer2 = InitWallTiles();

            droppedTilesPlayer1 = InitPlayerDroppedTiles();
            droppedTilesPlayer2 = InitPlayerDroppedTiles();

            factories = InitFactories();
            centerFactoryTiles = new ObservableCollection<Tile>();

            validProductionTilesPlayer1 = new ObservableCollection<ValidProductionTile>();
            validProductionTilesPlayer1.Add(new ValidProductionTile(0));
            validProductionTilesPlayer1.Add(new ValidProductionTile(1));
            validProductionTilesPlayer1.Add(new ValidProductionTile(2));
            validProductionTilesPlayer1.Add(new ValidProductionTile(3));
            validProductionTilesPlayer1.Add(new ValidProductionTile(4));
            validProductionTilesPlayer2 = new ObservableCollection<ValidProductionTile>();
            activatedDroppedPlayer1Tiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX);
            activatedDroppedPlayer2Tiles = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX);
            activatedDroppedTileObject = new ValidProductionTile(GameConstants.DROPPED_TILE_ROW_INDEX);
            UpdateViewModelFromModel();
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

        private void UpdateFactories()
        {
            //factories = _gameManager.GetFactories();
            //centerFactoryTiles = _gameManager.GetCenterFactoryTiles();
            _gameManager.UpdateFactories(factories);
            centerFactoryTiles = _gameManager.UpdateCenterFactoryTiles();
        }

        private void UpdatePlayerWallTiles()
        {
            //wallTilesPlayer1 = _gameManager.GetPlayerWallTiles(GameConstants.STARTING_PLAYER_INDEX);
            //wallTilesPlayer2 = _gameManager.GetPlayerWallTiles(GameConstants.PLAYER_TWO_INDEX);
            _gameManager.UpdatePlayerWallTiles(wallTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            _gameManager.UpdatePlayerWallTiles(wallTilesPlayer2, GameConstants.PLAYER_TWO_INDEX);
        }

        private void UpdateProductionTiles()
        {
            //productionTilesPlayer1 = _gameManager.GetPlayerProductionTiles(GameConstants.STARTING_PLAYER_INDEX);
            //productionTilesPlayer2 = _gameManager.GetPlayerProductionTiles(GameConstants.PLAYER_TWO_INDEX);
            _gameManager.UpdatePlayerProductionTiles(productionTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            _gameManager.UpdatePlayerProductionTiles(productionTilesPlayer2, GameConstants.PLAYER_TWO_INDEX);
        }

        private void UpdateDroppedTiles()
        {
            //droppedTilesPlayer1 = _gameManager.GetPlayerDroppedTiles(GameConstants.STARTING_PLAYER_INDEX);
            //droppedTilesPlayer1 = _gameManager.GetPlayerDroppedTiles(GameConstants.PLAYER_TWO_INDEX);
            _gameManager.UpdatePlayerDroppedTiles(droppedTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
            _gameManager.UpdatePlayerDroppedTiles(droppedTilesPlayer1, GameConstants.PLAYER_TWO_INDEX);
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

        public void FactoryTileSelected()
        {
            // TODO - fix how null tiles appear in the view.

            _gameManager.TestAddDroppedTile();
            _gameManager.UpdatePlayerDroppedTiles(droppedTilesPlayer1, GameConstants.STARTING_PLAYER_INDEX);
        }

        public void ProductionLineSelected(int productionTileIndex)
        {
            Trace.WriteLine(productionTileIndex);
        }
    }
}
