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

        /// <summary>
        /// Gets or sets the background colour of the tile
        /// </summary>
        public ObservableCollection<Factory> FactoryList
        {
            get { return (ObservableCollection<Factory>)GetValue(FactoryListProperty); }
            set
            {
                SetValue(FactoryListProperty, value);
            }
        }

        /// <summary>
        /// Identified the BackgroundColour dependency property
        /// </summary>
        public static readonly DependencyProperty FactoryListProperty =
            DependencyProperty.Register("FactoryList", typeof(ObservableCollection<Factory>),
              typeof(FactoryView), new PropertyMetadata(null));

        #endregion
        public FactoryView()
        {
            InitializeComponent();
            FactoryViewUI.DataContext = this;
        }
    }
}
