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
using WPF_Azul.Model;

namespace WPF_Azul.View.UserControls
{
    /// <summary>
    /// Interaction logic for WallTileSlot.xaml
    /// </summary>
    public partial class WallTileSlot : UserControl
    {
        #region BackgroundColour DP

        /// <summary>
        /// Gets or sets the background colour of the tile
        /// </summary>
        public TileType BackgroundColour
        {
            get { return (TileType)GetValue(BackgroundColourProperty); }
            set
            {
                SetValue(BackgroundColourProperty, value);
            }
        }

        /// <summary>
        /// Identified the BackgroundColour dependency property
        /// </summary>
        public static readonly DependencyProperty BackgroundColourProperty =
            DependencyProperty.Register("BackgroundColour", typeof(TileType),
              typeof(WallTileSlot), new PropertyMetadata(null));

        #endregion

        //private Color defaultColour;

        public WallTileSlot()
        {
            InitializeComponent();
            //defaultColour = Colors.Transparent;
            WallTileSlotUI.DataContext = this;
        }
    }
}
