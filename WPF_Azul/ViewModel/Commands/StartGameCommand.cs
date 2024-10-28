using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;
using WPF_Azul.Stores;

namespace WPF_Azul.ViewModel.Commands
{
    public class StartGameCommand : CommandBase
    {
        private NavigationStore navigationStore;
        private GameManager gameManager;

        public StartGameCommand(NavigationStore navigationStore, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            navigationStore.NavigateGameView();
            gameManager.StartGame();
        }
    }
}
