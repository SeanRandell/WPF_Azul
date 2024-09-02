using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_Azul.Model;

namespace WPF_Azul.View.UserControls
{
    /// <summary>
    /// Interaction logic for FactoryView.xaml
    /// </summary>
    public partial class FactoryView : UserControl
    {
        private ObservableCollection<ObservableCollection<Tile>> factoryTiles;

        public ObservableCollection<ObservableCollection<Tile>> FactoryTiles
        {
            get { return factoryTiles; }
            set { factoryTiles = value; }
        }

        public FactoryView()
        {
            InitializeComponent();
            factoryTiles = new ObservableCollection<ObservableCollection<Tile>>();
            TempInitFactoryTiles();
            DataContext = this;

        }

        private void TempInitFactoryTiles()
        {
            ObservableCollection<Tile> column1 = [new Tile(TileType.Yellow, 0, 0), new Tile(TileType.Red, 0, 0)];
            ObservableCollection<Tile> column2 = [new Tile(TileType.Black, 0, 0), new Tile(TileType.Blue, 0, 0)];
            factoryTiles.Add(column1);
            factoryTiles.Add(column2);

        }
    }
}
