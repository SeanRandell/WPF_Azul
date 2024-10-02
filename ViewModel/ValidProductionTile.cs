using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Azul.Model;

namespace WPF_Azul.ViewModel
{
	// TODO - this implements
    public class ValidProductionTile : ViewModelBase
    {
		private int productionTileIndex;

		public int ProductionTileIndex
        {
			get { return productionTileIndex; }
			set { productionTileIndex = value; }
		}

		private bool _isEnabled;

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value;
				OnPropertyChanged();
			}
		}

		public ValidProductionTile(int productionTileIndex, bool isEnabled)
        {
            this.productionTileIndex = productionTileIndex;
			IsEnabled = isEnabled;
        }
    }
}
