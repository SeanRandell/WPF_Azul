using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;

namespace WPF_Azul.ViewModel
{
    class TileSlotViewModel: ViewModelBase
    {
        public string tileType { get; private set; }

        // instantiate ClientComp in constructor:
        public TileSlotViewModel()
        {
        }
    }
}
