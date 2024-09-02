using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Stores;
using WPF_Azul.View;

namespace WPF_Azul.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        //private IPageViewModel _currentPageViewModel;
        //private List<IPageViewModel> _pageViewModels;

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;

            // Add available pages
            //PageViewModels.Add(new MainMenuViewModel());
            //PageViewModels.Add(new GameViewModel());

            // Set starting page
            //CurrentPageViewModel = PageViewModels[0];


            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        //public List<IPageViewModel> PageViewModels
        //{
        //    get
        //    {
        //        if (_pageViewModels == null)
        //            _pageViewModels = new List<IPageViewModel>();

        //        return _pageViewModels;
        //    }
        //}

        //public IPageViewModel CurrentPageViewModel
        //{
        //    get
        //    {
        //        return _currentPageViewModel;
        //    }
        //    set
        //    {
        //        if (_currentPageViewModel != value)
        //        {
        //            _currentPageViewModel = value;
        //            OnPropertyChanged("CurrentPageViewModel");
        //        }
        //    }
        //}


    }
}
