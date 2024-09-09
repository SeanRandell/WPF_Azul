using System;
using System.Collections.Generic;
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

        private List<List<Color>> wallPattern;
        public List<List<Color>> WallPattern
        {
            get { return wallPattern; }
            set { wallPattern = value; }
        }

        private List<List<Tile>> wallTilesPlayer1;

        public List<List<Tile>> WallTilesPlayer1
        {
            get { return wallTilesPlayer1; }
            set { wallTilesPlayer1 = value; }
        }

        private List<List<Tile>> wallTilesPlayer2;

        public List<List<Tile>> WallTilesPlayer2
        {
            get { return wallTilesPlayer2; }
            set { wallTilesPlayer2 = value; }
        }

        private List<List<Tile>> productionTilesPlayer1;

        public List<List<Tile>> ProductionTilesPlayer1
        {
            get { return productionTilesPlayer1; }
            set { productionTilesPlayer1 = value; }
        }

        private List<List<Tile>> productionTilesPlayer2;

        public List<List<Tile>> ProductionTilesPlayer2
        {
            get { return productionTilesPlayer2; }
            set { productionTilesPlayer2 = value; }
        }

        private List<Tile> droppedTilesPlayer1;

        public List<Tile> DroppedTilesPlayer1
        {
            get { return droppedTilesPlayer1; }
            set { droppedTilesPlayer1 = value; }
        }

        private List<Tile> droppedTilesPlayer2;

        public List<Tile> DroppedTilesPlayer2
        {
            get { return droppedTilesPlayer2; }
            set { droppedTilesPlayer2 = value; }
        }

        private List<Factory> factories;

        public List<Factory> Factories
        {
            get { return factories; }
            set { factories = value; }
        }

        private CenterFactory centerFactory;

        public CenterFactory CenterFactory
        {
            get { return centerFactory; }
            set { centerFactory = value; }
        }


        public GameViewModel(GameManager gameManager, NavigationStore navigationStore)
        {
            //this.navigationStore = navigationStore;
            _gameManager = gameManager;
            MainMenuCommand = new MainMenuCommand(navigationStore);

            wallPattern = new List<List<Color>>();
            productionTilesPlayer1 = new List<List<Tile>>();
            productionTilesPlayer2 = new List<List<Tile>>();

            wallTilesPlayer1 = new List<List<Tile>>();
            wallTilesPlayer2 = new List<List<Tile>>();

            droppedTilesPlayer1 = new List<Tile>();
            droppedTilesPlayer2 = new List<Tile>();

            factories = new List<Factory>();
            centerFactory = new CenterFactory();

            InitProductionTiles();
            InitPlayerWallTiles();
            InitViewWallPattern();
            InitDroppedTiles();

            InitFactories();
        }

        private void InitFactories()
        {
            factories = _gameManager.GetFactories();
            centerFactory = _gameManager.GetCenterFactory();
        }

        private void InitPlayerWallTiles()
        {
            wallTilesPlayer1 = _gameManager.GetPlayerWallTiles(GameConstants.STARTING_PLAYER_INDEX);
            wallTilesPlayer2 = _gameManager.GetPlayerWallTiles(GameConstants.PLAYER_TWO_INDEX);
        }

        private void InitProductionTiles()
        {
            productionTilesPlayer1 = _gameManager.GetPlayerProductionTiles(GameConstants.STARTING_PLAYER_INDEX);
            productionTilesPlayer2 = _gameManager.GetPlayerProductionTiles(GameConstants.PLAYER_TWO_INDEX);
        }

        private void InitDroppedTiles()
        {
            droppedTilesPlayer1 = _gameManager.GetPlayerDroppedTiles(GameConstants.STARTING_PLAYER_INDEX);
            droppedTilesPlayer1 = _gameManager.GetPlayerDroppedTiles(GameConstants.PLAYER_TWO_INDEX);
        }

        private void InitViewWallPattern()
        {
            TileType[,] tileTypeArray = GameConstants.WALL_TILE_PATTERN;

            for (int i = 0; i < 5; i++)
            {
                List<Color> currentRow = new List<Color>();
                for (int j = 0; j < 5; j++)
                {
                    currentRow.Add((Color)ColorConverter.ConvertFromString(tileTypeArray[i, j].ToString()));
                }
                WallPattern.Add(currentRow);
            }
        }
    }
}
