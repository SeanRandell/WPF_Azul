using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;

namespace WPF_Azul.ViewModel
{
    public class ValidProductionTile
    {
		private int productionTileIndex;

		public int ProductionTileIndex
        {
			get { return productionTileIndex; }
			set { productionTileIndex = value; }
		}

        public ValidProductionTile(int productionTileIndex)
        {
            this.productionTileIndex = productionTileIndex;
        }
    }
}
