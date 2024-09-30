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
        private readonly NavigationStore navigationStore;
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
        }

    }
}
