using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.View;
using WPF_Azul.ViewModel;

namespace WPF_Azul.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentPageViewModel;
        private MainMenuViewModel _mainMenuViewModel;
        private GameViewModel _gameViewModel;


        public ViewModelBase CurrentViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        public NavigationStore()
        {
        }

        public void SetInitialViewModel(MainMenuViewModel mainMenuViewModel, GameViewModel gameViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _gameViewModel = gameViewModel;
            CurrentViewModel = mainMenuViewModel;
        }

        // TODO - Find a more elegant way to do this if you want to
        public void NavigateMainMenu()
        {
            CurrentViewModel = _mainMenuViewModel;
        }

        public void NavigateGameView()
        {
            CurrentViewModel = _gameViewModel;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
