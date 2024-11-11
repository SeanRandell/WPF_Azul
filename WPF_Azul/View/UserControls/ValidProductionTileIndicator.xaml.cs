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
    public partial class ValidProductionTileIndicator : UserControl
    {
        #region ProductionLineCommandClick DP

        public ICommand ProductionLineCommandClick
        {
            get { return (ICommand)GetValue(ProductionLineCommandClickProperty); }
            set
            {
                SetValue(ProductionLineCommandClickProperty, value);
            }
        }

        public static readonly DependencyProperty ProductionLineCommandClickProperty =
            DependencyProperty.Register("ProductionLineCommandClick", typeof(ICommand),
              typeof(ValidProductionTileIndicator), new PropertyMetadata(null));

        #endregion

        #region ValidProductionLine DP

        public object ValidProductionLine
        {
            get { return (object)GetValue(ValidProductionLineProperty); }
            set
            {
                SetValue(ValidProductionLineProperty, value);
            }
        }

        public static readonly DependencyProperty ValidProductionLineProperty =
            DependencyProperty.Register("ValidProductionLine", typeof(object),
              typeof(ValidProductionTileIndicator), new PropertyMetadata(null));

        #endregion

        public ValidProductionTileIndicator()
        {
            InitializeComponent();
            ValidProductionTileIndicatorViewUI.DataContext = this;
        }
    }
}
