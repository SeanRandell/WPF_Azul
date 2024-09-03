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

        private readonly PlayerBoard player1Board;
        private readonly PlayerBoard player2Board;

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
            InitWallPattern();
            Console.WriteLine("test");
        }

        private void InitWallPattern()
        {
            TileType[,] tileTypeArray = _gameManager.GetWallPattern();

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
