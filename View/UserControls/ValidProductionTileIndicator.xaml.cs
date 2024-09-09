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
using WPF_Azul.ViewModel;

namespace WPF_Azul.View.UserControls
{
    /// <summary>
    /// Interaction logic for ValidProductionTileIndicator.xaml
    /// </summary>
    public partial class ValidProductionTileIndicator : UserControl
    {
        public ICommand ProductionLineCommandClick
        {
            get { return (ICommand)GetValue(ProductionLineCommandClickProperty); }
            set
            {
                SetValue(ProductionLineCommandClickProperty, value);
            }
        }

        /// <summary>
        /// Identified the BackgroundColour dependency property
        /// </summary>
        public static readonly DependencyProperty ProductionLineCommandClickProperty =
            DependencyProperty.Register("ProductionLineCommandClick", typeof(ICommand),
              typeof(ValidProductionTileIndicator), new PropertyMetadata(null));

        public ValidProductionTile ValidProductionLine
        {
            get { return (ValidProductionTile)GetValue(ValidProductionLineProperty); }
            set
            {
                SetValue(ValidProductionLineProperty, value);
            }
        }

        /// <summary>
        /// Identified the BackgroundColour dependency property
        /// </summary>
        public static readonly DependencyProperty ValidProductionLineProperty =
            DependencyProperty.Register("ValidProductionLine", typeof(ValidProductionTile),
              typeof(ValidProductionTileIndicator), new PropertyMetadata(null));
        public ValidProductionTileIndicator()
        {
            InitializeComponent();
            ValidProductionTileIndicatorViewUI.DataContext = this;
        }
    }
}
