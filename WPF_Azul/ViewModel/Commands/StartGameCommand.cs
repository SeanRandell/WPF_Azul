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
        private MainMenuViewModel mainMenuViewModel;

        public StartGameCommand(MainMenuViewModel mainMenuViewModel)
        {
            this.mainMenuViewModel = mainMenuViewModel;
        }

        public override void Execute(object? parameter)
        {
            mainMenuViewModel.OpenPlayerNameModal();
        }
    }
}
