using System.Configuration;
using System.Data;
using System.Windows;
using WPF_Azul.Model;
using WPF_Azul.Stores;
using WPF_Azul.ViewModel;

namespace WPF_Azul
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly GameManager gameManager;
        private readonly NavigationStore navigationStore;
        private readonly MainMenuViewModel mainMenuViewModel;
        private readonly GameViewModel gameViewModel;

        public App()
        {
            gameManager = new GameManager();
            navigationStore = new NavigationStore();
            gameViewModel = new GameViewModel(gameManager, navigationStore);
            mainMenuViewModel = new MainMenuViewModel(navigationStore, gameViewModel);

            NavigationSetup();
        }

        private void NavigationSetup()
        {
            navigationStore.SetInitialViewModel(mainMenuViewModel, gameViewModel);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
