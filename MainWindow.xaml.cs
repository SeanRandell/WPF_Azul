using System.Windows;
using WPF_Azul.View;

namespace WPF_Azul
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnModal_Click(object sender, RoutedEventArgs e)
        {
            PlayerNameModal modalWindow = new PlayerNameModal(this);
            Opacity = 0.4;
            modalWindow.ShowDialog();
            Opacity = 1;
            if (modalWindow.Success)
            {
                txtblckPlayerName.Text = modalWindow.Input;
            }
        }
    }
}