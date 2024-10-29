using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;
using WPF_Azul.Stores;

namespace WPF_Azul.ViewModel.Commands
{
    internal class SubmitPlayerNamesCommand : CommandBase
    {
        private readonly MainMenuViewModel _mainMenuViewModel;
        private NavigationStore navigationStore;
        private GameViewModel gameViewModel;

        internal SubmitPlayerNamesCommand(NavigationStore navigationStore, GameViewModel gameManager, MainMenuViewModel mainMenuViewModel)
        {
            this.gameViewModel = gameManager;
            this.navigationStore = navigationStore;
            _mainMenuViewModel = mainMenuViewModel;
        }

        public override void Execute(object? parameter)
        {
            _mainMenuViewModel.SetPlayerNamesFromModal();
            navigationStore.NavigateGameView();
        }
    }
}
