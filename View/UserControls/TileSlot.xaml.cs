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
    /// Interaction logic for TileSlot.xaml
    /// </summary>
    public partial class TileSlot : UserControl
    {
        #region BackgroundColour DP

        /// <summary>
        /// Gets or sets the background colour of the tile
        /// </summary>
        public Color BackgroundColour
        {
            get { return (Color)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Identified the BackgroundColour dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("BackgroundColour", typeof(Color),
              typeof(TileSlot), new PropertyMetadata(null));

        #endregion

        public TileSlot()
        {
            InitializeComponent();
            TileSlotUI.DataContext = this;
        }
    }
}
