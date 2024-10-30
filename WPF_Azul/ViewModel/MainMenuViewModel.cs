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

        private bool _isPlayerNameModalOpen;

        public bool IsPlayerNameModalOpen
        {
            get { return _isPlayerNameModalOpen; }
            set
            {
                _isPlayerNameModalOpen = value;
                OnPropertyChanged();
            }
        }

        private readonly NavigationStore navigationStore;
        private readonly GameViewModel gameViewModel;

        public string Name
        {
            get
            {
                return "Main Menu";
            }
        }

        public ICommand SubmitPlayerNamesCommand => new RelayCommand(execute => SubmitNamesAndStartGame());

        public ICommand StartGameCommand => new RelayCommand(execute => OpenPlayerNameModal());

        public ICommand QuitCommand => new RelayCommand(execute => QuitGame());

        public MainMenuViewModel(NavigationStore navigationStore, GameViewModel gameViewModel)
        {
            this.navigationStore = navigationStore;
            this.gameViewModel = gameViewModel;

            _isPlayerNameModalOpen = false;

            _player1Name = "";
            _player2Name = "";
        }

        internal void OpenPlayerNameModal()
        {
            IsPlayerNameModalOpen = true;
        }

        internal void SetPlayerNamesFromModal()
        {
            IsPlayerNameModalOpen = false;
            List<string> playerNames = new List<string> { Player1Name, Player2Name };
            gameViewModel.StartGame(playerNames);
        }

        internal void QuitGame()
        {
            App.Current.Shutdown();
        }

        internal void SubmitNamesAndStartGame()
        {
            SetPlayerNamesFromModal();
            navigationStore.NavigateGameView();
        }
    }
}
