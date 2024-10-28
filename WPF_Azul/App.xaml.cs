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

        public App()
        {
            gameManager = new GameManager();
            navigationStore = new NavigationStore();
            NavigationSetup();
        }

        private void NavigationSetup()
        {
            navigationStore.SetInitialViewModel(new MainMenuViewModel(navigationStore, gameManager), new GameViewModel(gameManager, navigationStore));
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
