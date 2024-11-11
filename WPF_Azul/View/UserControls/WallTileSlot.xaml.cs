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
    public partial class WallTileSlot : UserControl
    {
        #region TileType DP

        /// <summary>
        /// Gets or sets the Tile Type of the wall tile
        /// </summary>
        public TileType WallTileType
        {
            get { return (TileType)GetValue(BackgroundColourProperty); }
            set
            {
                SetValue(BackgroundColourProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Tile Type dependency property
        /// </summary>
        public static readonly DependencyProperty BackgroundColourProperty =
            DependencyProperty.Register("WallTileType", typeof(TileType),
              typeof(WallTileSlot), new PropertyMetadata(null));

        #endregion

        public WallTileSlot()
        {
            InitializeComponent();
            WallTileSlotUI.DataContext = this;
        }
    }
}
