using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using WPF_Azul.ViewModel.Commands;
using WPF_Azul.Model;
using WPF_Azul.Stores;
using WPF_Azul.View;

namespace WPF_Azul.ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {
        private string _player1Name;

        public string Player1Name
        {
            get { return _player1Name; }
            set { _player1Name = value; }
        }

        private string _player2Name;

        public string Player2Name
        {
            get { return _player2Name; }
            set { _player2Name = value; }
        }

        private readonly NavigationStore navigationStore;
        private readonly GameManager gameManager;
        public string Name
        {
            get
            {
                return "Main Menu";
            }
        }

        public ICommand StartGameCommand { get; }
        public ICommand QuitCommand { get; }

        public MainMenuViewModel(NavigationStore navigationStore, GameManager gameManager)
        {
            this.navigationStore = navigationStore;
            StartGameCommand = new StartGameCommand(navigationStore, gameManager);
            QuitCommand = new QuitCommand();

            _player1Name = "";
            _player2Name = "";
        }

        internal void SetPlayerNamesFromModal()
        {
            List<string> playerNames = new List<string>{Player1Name, Player2Name};
            gameManager.SetPlayerNames(playerNames);
        }

    }
}
