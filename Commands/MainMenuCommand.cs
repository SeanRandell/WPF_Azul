using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;
using WPF_Azul.Stores;

namespace WPF_Azul.Commands
{
    public class MainMenuCommand : CommandBase
    {
        private NavigationStore navigationStore;

        public MainMenuCommand(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            navigationStore.NavigateMainMenu();
        }
    }
}
