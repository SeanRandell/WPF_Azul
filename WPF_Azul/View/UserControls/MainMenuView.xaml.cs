using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Azul.View.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void OnShowModalClick(object sender, RoutedEventArgs e)
        {
            Modal.IsOpen = true;
        }

        private void OnCloseModalClick(object sender, RoutedEventArgs e)
        {
            Modal.IsOpen = false;
        }
    }
}
