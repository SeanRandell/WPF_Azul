using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;
using WPF_Azul.Stores;

namespace WPF_Azul.ViewModel.Commands
{
    public class MainMenuCommand : CommandBase
    {
        private GameViewModel _gameViewModel;
        private NavigationStore navigationStore;

        public MainMenuCommand(NavigationStore navigationStore, GameViewModel gameViewModel)
        {
            this.navigationStore = navigationStore;
            this._gameViewModel = gameViewModel;
        }

        public override void Execute(object? parameter)
        {
            _gameViewModel.ResetGameFromAnyState();
            navigationStore.NavigateMainMenu();
        }
    }
}
