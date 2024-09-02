using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.ViewModel;

namespace WPF_Azul.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly MainMenuViewModel mainMenuViewModel;

        public NavigationCommand(MainMenuViewModel mainMenuViewModel)
        {
            this.mainMenuViewModel = mainMenuViewModel;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
