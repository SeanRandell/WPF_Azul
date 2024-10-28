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
    public partial class FactoryView : UserControl
    {
        #region FactoryList DP

        public ObservableCollection<Tile> FactoryObject
        {
            get { return (ObservableCollection<Tile>)GetValue(FactoryProperty); }
            set
            {
                SetValue(FactoryProperty, value);
            }
        }

        public static readonly DependencyProperty FactoryProperty =
            DependencyProperty.Register("FactoryObject", typeof(ObservableCollection<Tile>),
              typeof(FactoryView), new PropertyMetadata(null));

        #endregion

        #region FactoryTileClickCommand DP

        public ICommand FactoryTileClickCommand
        {
            get { return (ICommand)GetValue(FactoryTileClickCommandProperty); }
            set
            {
                SetValue(FactoryTileClickCommandProperty, value);
            }
        }

        public static readonly DependencyProperty FactoryTileClickCommandProperty =
            DependencyProperty.Register("FactoryTileClickCommand", typeof(ICommand),
              typeof(FactoryView), new PropertyMetadata(null));

        #endregion

        public FactoryView()
        {
            InitializeComponent();
            FactoryViewUI.DataContext = this;
        }
    }
}
