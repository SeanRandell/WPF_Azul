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

        // TODO - Change to jsut use the wallpattern inside a player. See if an internal colour variable can be used.
        private List<List<Color>> wallPattern;
        public List<List<Color>> WallPattern
        {
            get { return wallPattern; }
            set { wallPattern = value; }
        }
        public GameViewModel(GameManager gameManager, NavigationStore navigationStore)
        {
            //this.navigationStore = navigationStore;
            _gameManager = gameManager;
            MainMenuCommand = new MainMenuCommand(navigationStore);
            InitViewWallPattern();
            Console.WriteLine("test");
        }

        private void InitViewWallPattern()
        {
            TileType[,] tileTypeArray = GameConstants.WALL_TILE_PATTERN;

            WallPattern = new List<List<Color>>();

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
