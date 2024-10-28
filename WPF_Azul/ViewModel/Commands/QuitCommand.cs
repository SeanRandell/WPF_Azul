using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Azul.ViewModel.Commands
{
    public class QuitCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            App.Current.Shutdown();
        }
    }
}
